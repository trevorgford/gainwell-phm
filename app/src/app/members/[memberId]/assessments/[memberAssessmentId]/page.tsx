'use client';

import React, { useEffect, useState } from 'react';
import axiosInstance from '../../../../../lib/axios';
import { useRouter, useParams } from 'next/navigation';
//import withAuth from '@/components/withAuth';
import { Accordion, AccordionDetails, AccordionSummary, Box, Breadcrumbs, Button, ButtonProps, Container, Divider, FormControl, Link, Typography, styled } from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { Assessment } from '@/types/Assessment';
import { MemberAssessment } from '@/types/MemberAssessment';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { MemberAssessmentAnswer } from '@/types/MemberAssessmentAnswer';
import CheckboxList from '@/components/checkboxList';
import RadioList from '@/components/radioList';
import { AssessmentQuestion } from '@/types/AssessmentQuestion';
import { toast } from 'react-toastify';
import { debug } from 'console';

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
  const [assessment, setAssessment] = useState<Assessment | null>(null);
  const [answers, setAnswers] = useState<MemberAssessmentAnswer[]>([]);

  const notify = (type: string, message: string) => {
    if(type == 'success') toast.success(message);
    else if(type == 'error') toast.error(message);
    else if(type == 'info') toast.info(message);
    else if(type == 'warn') toast.warning(message);
  };

  const handleSelectionChange = (selectedOptions: number[], questionId?: number) => {
    let answersLocal = answers;
    const answerIndex = answersLocal.findIndex(answer => answer.questionId === questionId);
    if (answerIndex > -1) answersLocal.splice(answerIndex, 1);

    if(selectedOptions.length === 0) setAnswers(answersLocal);
    else {
        answersLocal.push({ questionId, choiceId: undefined, choiceIds: selectedOptions, answer: undefined });
        setAnswers(answersLocal);
    }
  };
  
  const handleRadioListSelectionChange = (selectedOption: number, questionId?: number) => {
    let answersLocal = answers;
    const answerIndex = answersLocal.findIndex(answer => answer.questionId === questionId);
    answersLocal.splice(answerIndex, 1);
    answersLocal.push({ questionId, choiceId: selectedOption, choiceIds: undefined, answer: undefined });
    setAnswers(answersLocal);
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
        
        notify('success', 'Assessment saved');
    } 
    catch (error) {
        notify('error', 'Error saving assessment');
        console.error('Error saving assessment:', error);
    }

    //router.replace(`/members/${memberId}`);
  };

  const handleCancel = () => {
    router.replace(`/members/${memberId}`);
  };


  useEffect(() => {
    //const delay = (ms: number) => new Promise(res => setTimeout(res, ms));
   
    function transformAnswers(initialArray: MemberAssessmentAnswer[]): MemberAssessmentAnswer[] {
        // Create a map to group answers by questionId
        const groupedAnswers = new Map<number, MemberAssessmentAnswer>();
    
        initialArray.forEach(answer => {
            if (!answer.question || !answer.question.id || !answer.choice || !answer.choice.id) return;
    
            const questionId = answer.question.id;
            const choiceId = answer.choice.id;
    
            if (!groupedAnswers.has(questionId)) {
                groupedAnswers.set(questionId, {
                    ...answer,
                    questionId: questionId,
                    choiceId: choiceId,
                    choiceIds: [choiceId]
                });
            } else {
                const existingAnswer = groupedAnswers.get(questionId)!;
                existingAnswer.choiceIds!.push(choiceId);
            }
        });

        // Convert the map values to an array and process choiceIds to remove duplicates
        return Array.from(groupedAnswers.values()).map(answer => {
            // Remove duplicate choiceId values
            if (answer.choiceIds) {
                answer.choiceIds = Array.from(new Set(answer.choiceIds));
            }
            // Set choiceId to undefined if there are values in choiceIds
            if (answer.choiceIds && answer.choiceIds.length > 1) {
                answer.choiceId = undefined;
            }
            else if (answer.choiceIds && answer.choiceIds.length === 1) {
                answer.choiceId = answer.choiceIds[0];
                answer.choiceIds = undefined;
            }

            answer.question = undefined;
            answer.choice = undefined;
            answer.id = undefined;

            return answer;
        });        
    }

    let assessmentId: number = 0;
    const fetchMemberAssessment = async () => {
        setLoading(true);
        //await delay(5000);
        try {
            const token = localStorage.getItem('token') || '';
            axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            const response = await axiosInstance.get<MemberAssessment>(`/api/members/${memberId}/assessments/${memberAssessmentId}`);
            setMemberAssessment(response.data);
            assessmentId = response.data.assessment.id!;

            if(response.data.answers === undefined) return;
            const transformedArray = transformAnswers(response.data.answers);
            setAnswers(transformedArray);
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
        <Box>
          <Breadcrumbs>
            <Link href="/">
              <Typography color="textPrimary">Home</Typography>
            </Link>
            <Link href={`/members/${memberId}`}>
              <Typography color="textPrimary">Member Record</Typography>
            </Link>
            <Typography color="textPrimary">{assessment?.description} Assessment</Typography>
          </Breadcrumbs>
        </Box>        
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
                    {section.questions?.map((question: AssessmentQuestion, index: number) => {
                        const savedAnswer = answers.find(answer => answer.questionId == question.id);
                        let initialSelectedOptions = [] as number[] | undefined;
                        let initialSelectedOption: number | undefined = undefined;
                        const initialAnswer = savedAnswer ? savedAnswer.answer : undefined;

                        answers.forEach(answer => {
                            if(question.questionType?.id == 1 && question.id == answer.questionId && answer.choiceId !== undefined) initialSelectedOption = answer.choiceId;
                            else if(question.questionType?.id == 2 && question.id == answer.questionId) {
                                answer.choiceIds?.forEach(choiceId => {
                                    initialSelectedOptions?.push(choiceId);
                                });
                            }
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
                                        <RadioList parentId={question.id}
                                            initialSelectedOption={initialSelectedOption}
                                            onRadioListSelectionChange={handleRadioListSelectionChange}
                                            options={question.choices?.map(choice => ({ id: choice.id, description: choice.description })) || []} />
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
