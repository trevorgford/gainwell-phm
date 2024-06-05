'use client';

import React, { useEffect, useState } from 'react';
import axiosInstance from '../../../lib/axios';
import { useRouter, useParams } from 'next/navigation';
import { Member as MemberModel } from '@/types/Member';
import { CareTeam } from '@/types/CareTeam';
import { CarePlan } from '@/types/CarePlan';
import { CarePlanGoal } from '@/types/CarePlanGoal';
import { Person } from '@/types/Person';
import { Problem } from '@/types/Problem';
import { Goal } from '@/types/Goal';
import { Intervention } from '@/types/Intervention';
import { Barrier } from '@/types/Barrier';
//import withAuth from '@/components/withAuth';
import { Accordion, AccordionDetails, AccordionSummary, Autocomplete, Avatar, Box, Breadcrumbs, Button, ButtonProps, Checkbox, Container, Divider, FormControl, FormControlLabel, FormGroup, FormLabel, Grid, IconButton, InputLabel, Link, List, ListItem, ListItemAvatar, ListItemButton, ListItemIcon, ListItemText, MenuItem, Select, Tab, Tabs, TextField, Typography, styled } from '@mui/material';
import GroupsIcon from '@mui/icons-material/Groups';
import QuestionAnswerIcon from '@mui/icons-material/QuestionAnswer';
import MonitorHeartIcon from '@mui/icons-material/MonitorHeart';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { DataGrid, GridColDef, GridRenderCellParams, GridValueGetter } from '@mui/x-data-grid';
import ModalPopup from '@/components/modalPopup';
import { MemberAssessment } from '@/types/MemberAssessment';
import { Assessment } from '@/types/Assessment';
import { toast } from 'react-toastify';

const ColorButton = styled(Button)<ButtonProps>(({ theme }) => ({
    color: "#ffffff",
    backgroundColor: "#2B3A44",
    fontWeight: 600,
    '&:hover': {
      backgroundColor: "#00EEAE",
      color: "#2B3A44"
    },
}));

const Member: React.FC = () => {
  const params = useParams();
  const memberId = params.memberId;
  const router = useRouter();

  const [member, setMember] = useState<any| null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  const [tabValue, setValue] = React.useState(0);
  const [careTeam, setCareTeam] = useState<Person[]>([]);
  const [careTeamLoaded, setCareTeamLoaded] = useState<boolean>(false);

  const [addCareTeamModalOpen, setAddCareTeamModalOpen] = useState(false);
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [title, setTitle] = useState('');

  const [addCarePlanModalOpen, setAddCarePlanModalOpen] = useState(false);
  const [carePlans, setCarePlans] = useState<CarePlan[]>([]);
  const [carePlansLoaded, setCarePlansLoaded] = useState<boolean>(false);
  const [selectedCarePlanId, setSelectedCarePlanId] = useState<number | null | undefined>(null);

  const [problems, setProblems] = useState<Problem[]>([]);
  const [problemsLoaded, setProblemsLoaded] = useState<boolean>(false);
  const [selectedProblem, setSelectedProblem] = useState<Problem | null | undefined>(null);

  const [addGoalModalOpen, setAddGoalModalOpen] = useState(false);
  const [goals, setGoals] = useState<Goal[]>([]);
  const [goalsLoaded, setGoalsLoaded] = useState<boolean>(false);
  const [selectedGoal, setSelectedGoal] = useState<Goal | null | undefined>(null);

  const [interventions, setInterventions] = useState<Intervention[]>([]);
  const [interventionsLoaded, setInterventionsLoaded] = useState<boolean>(false);
  const [selectedInterventions, setSelectedInterventions] = useState<Intervention[]>([]);

  const [addAssessmentModalOpen, setAddAssessmentModalOpen] = useState(false);

  const [memberAssessments, setMemberAssessments] = useState<MemberAssessment[]>([]);
  const [memberAssessmentsLoaded, setMemberAssessmentsLoaded] = useState<boolean>(false);

  const problemsAutocompleteProps = {
    options: problems,
    getOptionLabel: (option: Problem) => option.description,
    getOptionId: (option: Problem) => option.id,
  };

  const goalsAutocompleteProps = {
    options: goals,
    getOptionLabel: (option: Goal) => option.description,
    getOptionId: (option: Goal) => option.id,
  };

  const interventionsAutocompleteProps = {
    options: interventions,
    getOptionLabel: (option: Intervention) => option.description,
    getOptionId: (option: Intervention) => option.id,
  };

  const notify = (type: string, message: string) => {
    if(type == 'success') toast.success(message);
    else if(type == 'error') toast.error(message);
    else if(type == 'info') toast.info(message);
    else if(type == 'warn') toast.warning(message);
  };  

  const handleTabChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
    
    if(newValue == 1) {
        if(!carePlansLoaded) loadCarePlans();
        if(!problemsLoaded) loadProblems();
        if(!goalsLoaded) loadGoals();
        if(!interventionsLoaded) loadInterventions();
    }
    else if(newValue == 2 && !careTeamLoaded) {
        if(!careTeamLoaded) loadCareTeam();
    }
  };

  const handleAddCareTeamOpenModal = () => {
    setAddCareTeamModalOpen(true);
  };

  const handleAddCareTeamCloseModal = () => {
    setAddCareTeamModalOpen(false);
  };

  const handleAddCareTeamSave = () => {
    //const formData = { firstName, lastName, title };
    //console.log('Save clicked');
    saveCareTeamMember();
    handleAddCareTeamCloseModal();
    notify('success', 'Care team member added');
  };

  const handleAddCareTeamCancel = () => {
    //console.log('Cancel clicked');
    handleAddCareTeamCloseModal();
  };

  const handleAddCarePlanOpenModal = () => {
    setAddCarePlanModalOpen(true);
  };

  const handleAddCarePlanCloseModal = () => {
    setAddCarePlanModalOpen(false);
  };

  const handleAddCarePlanSave = () => {
    saveCarePlan();
    handleAddCarePlanCloseModal();
  };

  const handleAddCarePlanCancel = () => {
    handleAddCarePlanCloseModal();
  }; 

  const handleInterventionSelected = (intervention: Intervention | null | undefined) => {
    if(intervention != null && intervention !== undefined) {
        setSelectedInterventions([...selectedInterventions, intervention]);
    }
  };

  const handleEditProblem = (event: React.MouseEvent<HTMLElement>, problemId: number) => {

  };

  const isInterventionSelected = (intervention: Intervention) => {
    return selectedInterventions.some((selectedIntervention) => selectedIntervention.id === intervention.id);
  };

  const handleSelectedInterventionCheckedChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
    const interventionId = parseInt(event.target.id);
    setSelectedInterventions(selectedInterventions.filter((intervention) => intervention.id !== interventionId));
  };
  
  const handleAddAssessmentOpenModal = () => {
    setAddAssessmentModalOpen(true);
  };

  const handleAddAssessmentCloseModal = () => {
    setAddAssessmentModalOpen(false);
  };

  const handleAddAssessmentSave = () => {
    //saveAssessment();
    handleAddAssessmentCloseModal();
  };

  const handleAddAssessmentCancel = () => {
    handleAddAssessmentCloseModal();
  }; 

  const handleAddGoalOpenModal = (carePlanId: number) => {
    setSelectedCarePlanId(carePlanId);
    setSelectedGoal(null);
    setAddGoalModalOpen(true);
  };

  const handleAddGoalCloseModal = () => {
    setAddGoalModalOpen(false);
  };

  const handleSetSelectedGoal = (goal: Goal | null | undefined) => {
    setSelectedGoal(goal);
  };

  const handleAddGoalSave = () => {
    saveGoal();
    setSelectedCarePlanId(null);
    setAddGoalModalOpen(false);
  };

  const handleAddGoalCancel = () => {
    handleAddGoalCloseModal();
  };

  const handleEditGoal = (event: React.MouseEvent<SVGSVGElement, MouseEvent>) => {
    event.stopPropagation();
    
  };

  const handleDeleteGoal = (event: React.MouseEvent<SVGSVGElement, MouseEvent>) => {
    event.stopPropagation();
    
  };

  const handleAddInterventionOpenModal = () => {

  };

  const handleAddBarrierOpenModal = () => {

  };

  const addCareTeamFooterButtons = [
    {
        label: 'Cancel',
        onClick: handleAddCareTeamCancel,
        variant: 'outlined',
        color: 'secondary',
      },    
    {
      label: 'Save',
      onClick: handleAddCareTeamSave,
      variant: 'contained',
      color: 'primary',
    },
  ]; 
  
  const addCarePlanFooterButtons = [
    {
        label: 'Cancel',
        onClick: handleAddCarePlanCancel,
        variant: 'outlined',
        color: 'secondary',
      },    
    {
      label: 'Save',
      onClick: handleAddCarePlanSave,
      variant: 'contained',
      color: 'primary',
    },
  ];   

  const addAssessmentFooterButtons = [
    {
        label: 'Cancel',
        onClick: handleAddAssessmentCancel,
        variant: 'outlined',
        color: 'secondary',
      },    
    {
      label: 'Save',
      onClick: handleAddAssessmentSave,
      variant: 'contained',
      color: 'primary',
    },
  ];
  
  const addGoalFooterButtons = [
    {
        label: 'Cancel',
        onClick: handleAddGoalCancel,
        variant: 'outlined',
        color: 'secondary',
      },    
    {
      label: 'Save',
      onClick: handleAddGoalSave,
      variant: 'contained',
      color: 'primary',
    },
  ];    

  useEffect(() => {
    const fetchMember = async () => {
        if(memberId) {
            try {
                const token = localStorage.getItem('token') || '';
                axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;
                const response = await axiosInstance.get<MemberModel>(`/api/members/${memberId}`);
                setMember(response.data);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
            finally {
                setLoading(false);  
            }
        }
    };

    const fetchMemberAssessments = async () => {
      if(memberId && !memberAssessmentsLoaded) {
        setLoading(true);
        try {
            const token = localStorage.getItem('token') || '';
            axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;

            const response = await axiosInstance.get<MemberAssessment[]>(`/api/members/${memberId}/assessments`);
            setMemberAssessments(response.data);
            setMemberAssessmentsLoaded(true);
        }
        catch (error) {
            console.error('Error fetching member assessments:', error);
        }
        finally {
            setLoading(false);
        }
      }
    };

    fetchMember();
    fetchMemberAssessments();
  }, [memberId]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (!member) {
    return <div>Member not found</div>;
  }

  const careTeamColumns: GridColDef[] = [
    { field: 'id', headerName: '', width: 100, renderCell: (params) => {
        const handleDeleteCareTeamMember = (e: { preventDefault: () => void; }) => {
          e.preventDefault();
          const personId = params.row.id;
          deleteCareTeamMember(personId);
        };
    
        return (
            <IconButton aria-label="delete" onClick={handleDeleteCareTeamMember}>
                <DeleteIcon sx={{ color: "#2B3A44", '&:hover': { color: '#00EEAE', } }} />
            </IconButton>
        );
      }, },
    { field: 'firstName', headerName: 'First Name', width: 150 },
    { field: 'lastName', headerName: 'Last Name', width: 150 },
    { field: 'title', headerName: 'Title', width: 150 }
  ];

  const interventionsColumns: GridColDef[] = [
    // { field: 'id', headerName: '', width: 100, renderCell: (params) => {
    //     const handleDeleteCareTeamMember = (e: { preventDefault: () => void; }) => {
    //       e.preventDefault();
    //       const personId = params.row.id;
    //       deleteCareTeamMember(personId);
    //       //alert(`Deleting person ${personId}`);
    //     };
    
    //     return (
    //         <IconButton aria-label="delete" onClick={handleDeleteCareTeamMember}>
    //             <DeleteIcon />
    //         </IconButton>
    //     );
    //   }, },
    { field: 'description', headerName: 'Intervention', flex:1 }
  ];

  const barriersColumns: GridColDef[] = [
    // { field: 'id', headerName: '', width: 100, renderCell: (params) => {
    //     const handleDeleteCareTeamMember = (e: { preventDefault: () => void; }) => {
    //       e.preventDefault();
    //       const personId = params.row.id;
    //       deleteCareTeamMember(personId);
    //       //alert(`Deleting person ${personId}`);
    //     };
    
    //     return (
    //         <IconButton aria-label="delete" onClick={handleDeleteCareTeamMember}>
    //             <DeleteIcon />
    //         </IconButton>
    //     );
    //   }, },
    { field: 'description', headerName: 'Barrier', flex:1 }
  ];  

  const memberAssessmentsColumns: GridColDef[] = [
    {
      field: 'assessment',
      headerName: 'Assessment Name',
      width: 200,
      valueGetter: (value: Assessment) => {(
        <Link href={`/assessments/${params.id}`}>
          {value.description}
        </Link>
      )},
      renderCell: (params) => {
        const handleClick = (e: { preventDefault: () => void; }) => {
          e.preventDefault();
          window.location.href = `/members/${memberId}/assessments/${params.row.id}`;
        };
    
        return (
          <Link href={`/members/${memberId}/assessments/${params.row.id}`} onClick={handleClick}>
            {params.row.assessment.description}
          </Link>
        );
      },
    },   
    { field: 'modifiedTimestamp', headerName: 'Last Modified', width: 200 }
  ];  

  const loadCareTeam = async () => {
    setLoading(true);
    try {
        const token = localStorage.getItem('token') || '';
        axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;

        const response = await axiosInstance.get<CareTeam>(`/api/members/${member.id}/careteam/load`);
        setCareTeam(response.data.people);
        setCareTeamLoaded(true);
    } catch (error) {
        console.error('Error fetching care team:', error);
    } finally {
        setLoading(false);
    }
  };
  
  const saveCareTeamMember = async () => {
    setLoading(true);
    try {
        const token = localStorage.getItem('token') || '';
        axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;

        const response = await axiosInstance.post(`/api/members/${member.id}/careteam/addPerson`, { memberId, firstName, lastName, title });
        setCareTeam([...careTeam, { id: response.data, firstName: firstName, lastName: lastName, title: title }]);
    } 
    catch (error) {
        console.error('Error adding care team member:', error);
    } 
    finally {
        setLoading(false);
    }
  };

  const deleteCareTeamMember = async (personId: number) => {
    setLoading(true);
    try {
        const token = localStorage.getItem('token') || '';
        axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;

        const response = await axiosInstance.post(`/api/members/${member.id}/careteam/removePerson`, { memberId, personId });
        setCareTeam(careTeam.filter((person) => person.id !== personId));
    } 
    catch (error) {
        console.error('Error deleting care team member:', error);
    } 
    finally {
        setLoading(false);
    }
  };  

  const loadProblems = async () => {
    setLoading(true);
    try {
        const token = localStorage.getItem('token') || '';
        axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;

        const response = await axiosInstance.get<Problem[]>(`/api/problems`);
        setProblems(response.data);
        setProblemsLoaded(true);
    } 
    catch (error) {
        console.error('Error fetching care plan problems:', error);
    } 
    finally {
        setLoading(false);
    }
  };

  const loadGoals = async () => {
    setLoading(true);
    try {
        const token = localStorage.getItem('token') || '';
        axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;

        const response = await axiosInstance.get<Goal[]>(`/api/goals`);
        setGoals(response.data);
        setGoalsLoaded(true);
    } 
    catch (error) {
        console.error('Error fetching care plan goals:', error);
    } 
    finally {
        setLoading(false);
    }
  };  

  const loadInterventions = async () => {
    setLoading(true);
    try {
        const token = localStorage.getItem('token') || '';
        axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;

        const response = await axiosInstance.get<Intervention[]>(`/api/interventions`);
        setInterventions(response.data);
        setInterventionsLoaded(true);
    } 
    catch (error) {
        console.error('Error fetching care plan interventions:', error);
    } 
    finally {
        setLoading(false);
    }
  };
  
  const loadCarePlans = async () => {
    setLoading(true);
    try {
        const token = localStorage.getItem('token') || '';
        axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;

        const response = await axiosInstance.get<CarePlan[]>(`/api/members/${member.id}/careplans/load`);
        setCarePlans(response.data);
        setCarePlansLoaded(true);
    } 
    catch (error) {
        console.error('Error loading care plans:', error);
    } 
    finally {
        setLoading(false);
    }
  };

  const saveCarePlan = async () => {
    setLoading(true);
    try {
        let carePlan = { 
            member: { id: memberId }, 
            problem: { id: selectedProblem?.id },
            carePlanGoals: [] as Goal[], 
            //goal: { id: selectedGoal?.id }, 
            interventions: [] as Intervention[],
            barriers: [] as Barrier[],
            carePlanName: null 
        };

        if (selectedGoal?.id != null) {
            carePlan.carePlanGoals.push({ id: selectedGoal.id, description: selectedGoal.description });

            selectedInterventions.forEach((intervention) => {
                carePlan.interventions.push({ id: intervention.id, description: intervention.description });
            });

            // selectedBarriers.forEach((barrier) => {
            //     carePlan.barriers.push({ id: barrier.id, description: barrier.description });
            // });
        }

        const token = localStorage.getItem('token') || '';
        axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;

        const response = await axiosInstance.post(`/api/members/${member.id}/careplans`, carePlan);
        //setCareTeam([...careTeam, { id: response.data, firstName: firstName, lastName: lastName, title: title }]);
    } 
    catch (error) {
        console.error('Error adding care plan:', error);
    } 
    finally {
        setLoading(false);
    }
  };

  const saveGoal = async () => {
    setLoading(true);
    try {
        let carePlanGoal = { goal: { id: selectedGoal?.id, description: selectedGoal?.description }, carePlan: { id: selectedCarePlanId } };
        const token = localStorage.getItem('token') || '';
        axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;

        const response = await axiosInstance.post(`/api/members/${member.id}/careplans/addGoal`, carePlanGoal);
        setCarePlans(carePlans.map((carePlan) => {
            if(carePlan.id === selectedCarePlanId) {
                carePlan.carePlanGoals.push({ id: response.data, goal: { id: selectedGoal!.id, description: selectedGoal!.description } });
            }
            return carePlan;
        }));
        //setCareTeam([...careTeam, { id: response.data, firstName: firstName, lastName: lastName, title: title }]);
    } 
    catch (error) {
        console.error('Error adding care plan goal:', error);
    } 
    finally {
        setLoading(false);
    }
  };

  return (
    <Box>
        <Box>
          <Breadcrumbs>
            <Link href="/">
              <Typography color="textPrimary">Home</Typography>
            </Link>
            <Typography color="textPrimary">Member Record</Typography>
          </Breadcrumbs>
        </Box>
        {/* <Box>
            <Typography variant="h5" component="h1">Member Record</Typography>
            <p>Member ID: {member.id}</p>
            <p>First Name: {member.firstName}</p>
            <p>Last Name: {member.lastName}</p>  
        </Box> */}
        <Box sx={{ mt: 2 }}>
            <Tabs value={tabValue} textColor="secondary" indicatorColor="secondary" onChange={handleTabChange} aria-label="member record tabs" 
            sx={{
            backgroundColor: '#2B3A44', // Background color of the Tabs
            '& .MuiTab-root': {
              color: '#FFFFFF', // Color of the Tab text
            },
            '& .Mui-selected': {
              color: '#00EEAE', // Color of the selected Tab text
            },
          }}>
                <Tab icon={<QuestionAnswerIcon />} iconPosition="start" label="Assessments" />
                <Tab icon={<MonitorHeartIcon />} iconPosition="start" label="Care Plans" />
                <Tab icon={<GroupsIcon />} iconPosition="start" label="Care Team" />
            </Tabs>
        </Box>
        <Box sx={{ p: 2, borderRight: '2px solid #2B3A44', borderBottom: '2px solid #2B3A44', borderLeft: '2px solid #2B3A44' }}>
            {tabValue == 0 && (
                <>
                    <Box sx={{ alignContent: 'right' }}>
                        <ColorButton variant="contained" color="primary" onClick={handleAddAssessmentOpenModal} disabled={loading} sx={{ mt: 1 }}>
                            {'Add Assessment'}
                        </ColorButton>
                    </Box>
                    <DataGrid 
                        rows={memberAssessments} 
                        columns={memberAssessmentsColumns} 
                        getRowId={(row) => row.id} 
                        sx={{ mt: 3, 
                            '& .MuiDataGrid-columnHeaders': {
                            color: '#ffffff', // Text color
                            '&:hover': { color: '#00EEAE', }, // Hover text color
                          },
                          '& .MuiDataGrid-container--top [role=row]': {
                            backgroundColor: '#2B3A44 !important',
                            background: '#2B3A44 !important', // Background color
                          },
                          '& .MuiDataGrid-columnHeaders .MuiIconButton-root': {
                            color: '#ffffff', // Text color
                            '&:hover': { color: '#00EEAE', }, // Hover text color
                          },
                          '& .MuiDataGrid-columnHeaderTitle': {
                            fontWeight: 'bold', // Optional: Make header text bold
                          }, }}
                    />         
                </>
            )}
            {tabValue == 1 && (
                <>
                    <Box sx={{ alignContent: 'right' }}>
                        <ColorButton variant="contained" color="primary" onClick={handleAddCarePlanOpenModal} disabled={loading} sx={{ mt: 1 }}>
                            {'Add Care Plan'}
                        </ColorButton>
                    </Box>
                    {carePlans?.map((carePlan) => (
                        <Accordion sx={{ mt: 2 }}>
                            <AccordionSummary expandIcon={<ExpandMoreIcon sx={{ color: "#ffffff", '&:hover': { color: '#00EEAE', } }} />}
                                sx={{ backgroundColor: "#2B3A44", color: "#ffffff", '&:hover': { color: '#00EEAE', } }}>
                                <Box>
                                    {carePlan.carePlanName ?? carePlan.problem.description}                                    
                                </Box>
                            </AccordionSummary>
                            <AccordionDetails>
                                <Box>
                                    <Grid item xs={12} md={6}>
                                        <Typography variant="h6" align="left">
                                            Problem
                                        </Typography>   
                                        <List dense={true}>
                                            <ListItemButton onClick={(event) => handleEditProblem(event, 0)}>
                                                <ListItemIcon>
                                                    <EditIcon sx={{ color: "#2B3A44", '&:hover': { color: '#00EEAE', } }} />
                                                    <DeleteIcon sx={{ color: "#2B3A44", '&:hover': { color: '#00EEAE', } }} />
                                                </ListItemIcon>
                                                <ListItemText primary={carePlan.problem.description} />
                                            </ListItemButton>
                                        </List>                           
                                    </Grid>
                                </Box>
                                <Divider />
                                <ColorButton variant="contained" color="primary" onClick={() => handleAddGoalOpenModal(carePlan.id)} disabled={loading} sx={{ mt: 2 }}>
                                    {'Add Goal'}
                                </ColorButton>                                 
                                <Typography variant="h6" align="left" sx={{ mt: 1 }}>
                                    Goals
                                </Typography> 
                                {carePlan.carePlanGoals?.map((carePlanGoal) => (
                                    <Box>        
                                        <Accordion sx={{ borderTop: 'solid 1px #ffffff' }}>
                                            <AccordionSummary expandIcon={<ExpandMoreIcon sx={{ color: "#ffffff", '&:hover': { color: '#00EEAE', } }} />}
                                                sx={{ backgroundColor: "#2B3A44", color: "#ffffff", '&:hover': { color: '#00EEAE', } }}>
                                                <Box display="flex" justifyContent="space-between" width="100%" sx={{ lineHeight: 2.5 }}>
                                                    <Box>
                                                        <IconButton aria-label="delete">
                                                            <EditIcon sx={{ color: "#ffffff", '&:hover': { color: '#00EEAE', } }} onClick={handleEditGoal} />
                                                            <DeleteIcon sx={{ color: "#ffffff", '&:hover': { color: '#00EEAE', } }} onClick={handleDeleteGoal} />
                                                        </IconButton>
                                                        {carePlanGoal.goal?.description}                                                         
                                                    </Box>
                                                    <Box display="flex" justifyContent="space-between" width="40%" sx={{ pr: 3 }}>
                                                        <Box>
                                                            Due {carePlanGoal.goal?.dueDateString}
                                                        </Box>
                                                        <Box>
                                                            {carePlanGoal.goal?.goalStatus}
                                                        </Box>
                                                    </Box>
                                                </Box>
                                            </AccordionSummary>
                                            <AccordionDetails>
                                                <ColorButton variant="contained" color="primary" onClick={() => handleAddInterventionOpenModal()} disabled={loading} sx={{ mt: 1 }}>
                                                    {'Add Intervention'}
                                                </ColorButton> 
                                                <Grid item xs={12} md={6}>
                                                    <Typography variant="h6" align="left" sx={{ mt: 1 }} component="div">
                                                        Interventions
                                                    </Typography>
                                                    <List dense={true}>
                                                        {carePlan.interventions?.map((intervention) => (
                                                            <ListItemButton onClick={(event) => handleEditProblem(event, 0)}>
                                                                <ListItemIcon>
                                                                    <EditIcon sx={{ color: "#2B3A44", '&:hover': { color: '#00EEAE', } }} />
                                                                    <DeleteIcon sx={{ color: "#2B3A44", '&:hover': { color: '#00EEAE', } }} />
                                                                </ListItemIcon>
                                                                <ListItemText primary={intervention.description} />
                                                            </ListItemButton>                                                           
                                                        ))}
                                                    </List>                                                 
                                                </Grid>
                                                <ColorButton variant="contained" color="primary" onClick={() => handleAddBarrierOpenModal()} disabled={loading} sx={{ mt: 1 }}>
                                                    {'Add Barrier'}
                                                </ColorButton>
                                                <Grid item xs={12} md={6}>
                                                    <Typography variant="h6" align="left" sx={{ mt: 1 }} component="div">
                                                        Barriers
                                                    </Typography>
                                                    <List dense={true}>
                                                        {carePlan.barriers?.map((barrier) => (
                                                            <ListItem
                                                            secondaryAction={
                                                                <IconButton edge="start" aria-label="delete">
                                                                    <DeleteIcon />
                                                                </IconButton>
                                                            }>
                                                                <ListItemText primary={barrier.description} />
                                                            </ListItem>                                                            
                                                        ))}
                                                    </List>                                                 
                                                </Grid>
                                            </AccordionDetails>
                                        </Accordion>                                        
                                    </Box>
                                ))}
                            </AccordionDetails>
                        </Accordion> 
                    ))}       
                </>
            )}
            {tabValue == 2 && (
                <>
                    <Box sx={{ alignContent: 'right' }}>
                        <ColorButton
                            variant="contained"
                            color="primary"
                            onClick={handleAddCareTeamOpenModal}
                            disabled={loading}
                            sx={{ mt: 1 }}
                        >
                            {'Add Care Team Member'}
                        </ColorButton>
                    </Box>
                    <DataGrid 
                        rows={careTeam} 
                        columns={careTeamColumns} 
                        getRowId={(row) => row.id} 
                        sx={{ mt: 3, 
                            '& .MuiDataGrid-columnHeaders': {
                            color: '#ffffff', // Text color
                            '&:hover': { color: '#00EEAE', }, // Hover text color
                          },
                          '& .MuiDataGrid-container--top [role=row]': {
                            backgroundColor: '#2B3A44 !important',
                            background: '#2B3A44 !important', // Background color
                          },
                          '& .MuiDataGrid-columnHeaders .MuiIconButton-root': {
                            color: '#ffffff', // Text color
                            '&:hover': { color: '#00EEAE', }, // Hover text color
                          },
                          '& .MuiDataGrid-columnHeaderTitle': {
                            fontWeight: 'bold', // Optional: Make header text bold
                          }, }}
                    />                
                </>
            )}
        </Box>
        <ModalPopup open={addAssessmentModalOpen} onClose={handleAddAssessmentCloseModal} headerText="Add Assessment" footerButtons={addAssessmentFooterButtons}>
            <span>Add an assessment</span>
        </ModalPopup>
        <ModalPopup open={addCarePlanModalOpen} onClose={handleAddCarePlanCloseModal} headerText="Add Care Plan" footerButtons={addCarePlanFooterButtons}>
            <Autocomplete {...problemsAutocompleteProps} id="problem-autocomplete" options={problems} fullWidth
                value={selectedProblem}
                onChange={(event: any, newValue: Problem | null | undefined) => {
                  setSelectedProblem(newValue);
                }} 
                renderInput={(params) => <TextField {...params} label="Problem" />} />
            {selectedProblem != null && selectedProblem !== undefined && (
                <Autocomplete {...goalsAutocompleteProps} id="goal-autocomplete" options={goals} fullWidth
                    sx={{ mt: 2 }}
                    value={selectedGoal}
                    onChange={(event: any, newValue: Goal | null | undefined) => {
                        setSelectedGoal(newValue);
                    }} 
                    renderInput={(params) => <TextField {...params} label="Goal" />}
                />
            )}
            {selectedGoal != null && selectedGoal !== undefined && (
                <>
                    <Autocomplete {...interventionsAutocompleteProps} id="intervention-autocomplete" options={interventions} fullWidth
                        sx={{ mt: 2 }}
                        onChange={(event: any, newValue: Intervention | null | undefined) => {
                            handleInterventionSelected(newValue);
                        }}
                        renderInput={(params) => <TextField {...params} label="Intervention" />}
                    />
                    {selectedInterventions.length > 0 && (
                        <FormControl sx={{ m: 2 }} component="fieldset" variant="standard">
                            <FormLabel component="legend">Selected Interventions</FormLabel>
                            <FormGroup>
                                {selectedInterventions?.map((intervention) => (
                                    <FormControlLabel
                                        key={intervention.id}
                                        control={<Checkbox checked={isInterventionSelected(intervention)} onChange={handleSelectedInterventionCheckedChanged} id={intervention.id.toString()} />}
                                        label={intervention.description}
                                    />
                                ))}
                            </FormGroup>
                        </FormControl>                         
                    )}           
                </>
            )}
        </ModalPopup>
        <ModalPopup open={addCareTeamModalOpen} onClose={handleAddCareTeamCloseModal} headerText="Add Care Team Member" footerButtons={addCareTeamFooterButtons}>
            <Box>
                <TextField
                label="First Name"
                fullWidth
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
                margin="normal"
                />
                <TextField
                label="Last Name"
                fullWidth
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
                margin="normal"
                />
                <TextField
                label="Title"
                fullWidth
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                margin="normal"
                />
            </Box>
        </ModalPopup>
        <ModalPopup open={addGoalModalOpen} onClose={handleAddGoalCloseModal} headerText="Add Goal" footerButtons={addGoalFooterButtons}>
            <Autocomplete {...goalsAutocompleteProps} id="goal-autocomplete2" options={goals} fullWidth
                sx={{ mt: 2 }}
                value={selectedGoal}
                onChange={(event: any, newValue: Goal | null | undefined) => {
                    handleSetSelectedGoal(newValue);
                }} 
                renderInput={(params) => <TextField {...params} label="Goal" />}
            />
        </ModalPopup>
    </Box>

  );
};

//export default withAuth(Member);
export default Member;
