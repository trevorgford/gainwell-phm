'use client';

import React, { useEffect, useState } from 'react';
import axiosInstance from '../../../../../lib/axios';
import { useRouter, useParams } from 'next/navigation';
//import withAuth from '@/components/withAuth';
import { Accordion, AccordionDetails, AccordionSummary, AppBar, Autocomplete, Avatar, Box, Button, ButtonProps, Checkbox, Container, Divider, FormControl, FormControlLabel, FormGroup, FormLabel, Grid, IconButton, InputLabel, Link, List, ListItem, ListItemAvatar, ListItemButton, ListItemIcon, ListItemText, MenuItem, Radio, RadioGroup, Select, Tab, Tabs, TextField, Toolbar, Typography, styled } from '@mui/material';
import GroupsIcon from '@mui/icons-material/Groups';
import QuestionAnswerIcon from '@mui/icons-material/QuestionAnswer';
import MonitorHeartIcon from '@mui/icons-material/MonitorHeart';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import CancelIcon from '@mui/icons-material/Cancel';
import SaveIcon from '@mui/icons-material/Save';

import { DataGrid, GridColDef, GridRenderCellParams, GridValueGetter } from '@mui/x-data-grid';
import ModalPopup from '@/components/modalPopup';
import { Assessment } from '@/types/Assessment';
import { MemberAssessment } from '@/types/MemberAssessment';
import { set } from 'date-fns';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';

import Tooltip from '@mui/material/Tooltip';
import Stack from '@mui/material/Stack';
import { DemoContainer, DemoItem } from '@mui/x-date-pickers/internals/demo';
import { TimePicker } from '@mui/x-date-pickers/TimePicker';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import { MemberAssessmentAnswer } from '@/types/MemberAssessmentAnswer';
import CheckboxList from '@/components/checkboxList';

const ColorButton = styled(Button)<ButtonProps>(({ theme }) => ({
    color: "#ffffff",
    backgroundColor: "#2B3A44",
    fontWeight: 600,
    '&:hover': {
      backgroundColor: "#00EEAE",
      color: "#2B3A44"
    },
}));

  const AssessmentPage: React.FC = () => {
  const router = useRouter();
  const [loading, setLoading] = useState<boolean>(true);
  const params = useParams();
  const memberId = params.memberId;
  const memberAssessmentId = params.memberAssessmentId;

  const [memberAssessment, setMemberAssessment] = useState<MemberAssessment | null>(null);
  //const [assessmentId, setAssessmentId] = useState<number | undefined>(undefined);
  const [assessment, setAssessment] = useState<Assessment | null>(null);


  const [answers, setAnswers] = useState<MemberAssessmentAnswer[]>([]);

  const handleSelectionChange = (selectedOptions: number[], questionId?: number) => {
    setAnswers(prevAnswers => {
      const answerIndex = prevAnswers.findIndex(answer => answer.questionId === questionId);
      if (answerIndex !== -1) prevAnswers.splice(answerIndex, 1);
      if(selectedOptions.length === 0) return prevAnswers;
      return [...prevAnswers, { questionId, choiceId: undefined, choiceIds: selectedOptions, answer: undefined }];
    });
  };  

  const handleChange = (questionId: number, choiceId: number) => {
      setAnswers(prevAnswers => {
          const answerIndex = prevAnswers.findIndex(answer => answer.question?.id === questionId);
          if (answerIndex !== -1) prevAnswers.splice(answerIndex, 1);
          return [...prevAnswers, { questionId, choiceId, choiceIds: undefined, answer: undefined }];
      });
  };

  const checkAnswerSelected = (choiceId: {id: number}) =>  {

    return true;
  };
  
  const saveAssessment = async () => {
    const assessmentAnswers: MemberAssessmentAnswer[] = [];
    answers.forEach(answer => {
        if(answer.choiceId !== undefined) {
            assessmentAnswers.push({ question: { id: answer.questionId }, choice: { id: answer.choiceId }, answerText: answer.answer });
        }
        else {
            answer.choiceIds?.forEach(choiceId => {
                assessmentAnswers.push({ question: { id: answer.questionId }, choice: { id: choiceId }, answerText: answer.answer });
            });
        }
    });

    try {
        const token = localStorage.getItem('token') || '';
        axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        const response = await axiosInstance.post<MemberAssessmentAnswer[]>(`/api/members/${memberId}/assessments/${memberAssessmentId}/answers`, assessmentAnswers);
    } 
    catch (error) {
        console.error('Error saving member assessment answers:', error);
    }

    //router.replace(`/members/${memberId}`);
  };

  const handleCancel = () => {
    router.replace(`/members/${memberId}`);
  };


  useEffect(() => {
    //const delay = (ms: number) => new Promise(res => setTimeout(res, ms));
   
    let assessmentId: number = 0;
    const fetchMemberAssessment = async () => {
        setLoading(true);
        //await delay(5000);
        try {
            const token = localStorage.getItem('token') || '';
            axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            const response = await axiosInstance.get<MemberAssessment>(`/api/members/${memberId}/assessments/${memberAssessmentId}`);
            setMemberAssessment(response.data);
            setAnswers(response.data.answers || []);
            //setAssessmentId(response.data.assessment.id);
            assessmentId = response.data.assessment.id!;
        } 
        catch (error) {
            console.error('Error fetching member assessment:', error);
        }
    };

    const fetchAssessment = async () => {
        try {
            const token = localStorage.getItem('token') || '';
            axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            const response = await axiosInstance.get<Assessment>(`/api/assessments/${assessmentId}`);

            setAssessment(response.data);
            //debugger;
        } 
        catch (error) {
            console.error('Error fetching assessment:', error);
        }
    };

    fetchMemberAssessment().then(() => fetchAssessment()).then(() => setLoading(false));
  }, []);

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <Box>
        <Typography variant="h4" align="center" gutterBottom sx={{ mb: 2 }}>
            {assessment?.description} Assessment
        </Typography>
        {assessment?.sections?.map((section) => (
            <Accordion key={section.id} defaultExpanded>
                <AccordionSummary expandIcon={<ExpandMoreIcon sx={{ color: "#ffffff", '&:hover': { color: '#00EEAE', } }} />}
                    sx={{ backgroundColor: "#2B3A44", color: "#ffffff", '&:hover': { color: '#00EEAE', } }}>
                    <Typography variant="h5">{section.description}</Typography>
                </AccordionSummary>
                <AccordionDetails>
                    {section.questions?.map((question, index: number) => {
                        const savedAnswer = answers.find(answer => answer.question?.id === question.id);
                        let initialSelectedOptions = [] as number[] | undefined;
                        let initialSelectedOption: number | undefined = undefined;
                        const initialAnswer = savedAnswer ? savedAnswer.answerText : undefined;

                        answers.forEach(answer => {
                            if(question.questionType?.id == 1 && question.id == answer.question?.id) initialSelectedOption = answer.choice?.id;
                            else if(question.questionType?.id == 2 && question.id == answer.question?.id) initialSelectedOptions?.push(answer.choice?.id!);
                        });

                        return (
                        <Box key={question.id}>
                            {index > 0 && (
                                <>
                                    <Divider key={question.id} sx={{ my: 3 }} />                                
                                </>
                            )}
                            <Typography sx={{ mt: 2 }}>{question.description}</Typography>
                            {question.questionType?.id === 1 && (
                                <>
                                    <FormControl sx={{ ml: 5, mt: 2 }}>
                                        <RadioGroup key={question.id}>
                                            {question.choices?.map((choice) => (
                                                
                                                <FormControlLabel key={choice.id} value={choice.id} control={<Radio 
                                                    onChange={(e) => handleChange(question.id!, choice.id!)}
                                                    checked={initialSelectedOption == choice.id} />} label={choice.description} />
                                            ))}
                                        </RadioGroup>
                                    </FormControl>
                                </>
                            )}
                            {question.questionType?.id === 2 && (
 
                                

                                <Container sx={{ ml: 2, mt: 2 }}>
                                    <CheckboxList parentId={question.id} 
                                        initialSelectedOptions={initialSelectedOptions}
                                        onSelectionChange={handleSelectionChange} 
                                        options={question.choices?.map(choice => ({ id: choice.id, description: choice.description })) || []} />
                                </Container>
                            )}                            
                            {question.questionType?.id === 4 && (
                                <>
                                    <LocalizationProvider dateAdapter={AdapterDateFns}>
                                        <DatePicker sx={{ ml: 5, mt: 2 }} />
                                    </LocalizationProvider>
                                </>
                            )}
                        </Box>
                    )})}
                </AccordionDetails>
            </Accordion>
        ))}
        <Box sx={{ display: 'flex', justifyContent: 'center', mt: 3 }}>
            <ColorButton variant="contained" color="primary" sx={{ mr: 2 }} onClick={saveAssessment}>Save</ColorButton>
            <ColorButton variant="contained" color="primary" sx={{ ml: 2 }} onClick={handleCancel}>Cancel</ColorButton>
        </Box>
    </Box>
  );
};

//export default withAuth(Member);
export default AssessmentPage;
