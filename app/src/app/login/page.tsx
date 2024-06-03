'use client';

import { useState } from 'react';
import { useRouter } from 'next/navigation';
import { getAuthenticationContext } from '../../context/AuthContext';
import axiosInstance from '../../lib/axios';
import { Box, Button, ButtonProps, Container, TextField, Typography, styled } from '@mui/material';
import { Poppins } from 'next/font/google';
import '../globals.css';

const poppins = Poppins({
    subsets: ['latin'],
    weight: "500"
});

const Login = () => {
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const router = useRouter();
    const { login } = getAuthenticationContext();

    const handleLogin = async (event: React.FormEvent) => {
        event.preventDefault();
        await authenticate();
    };

    const authenticate = async () => {
        try {
            const response = await axiosInstance.post('/api/Authentication/login', { userName, password });

            if (response.data.sessionId) {
                const response2 = await axiosInstance.post('/api/Authentication/selectTenant', { sessionId: response.data.sessionId, tenantId: 5 });
                localStorage.setItem('token', response2.data.token);
                login();
                router.push('/');
            } 
            else {
                alert('Invalid credentials');
            }
        } 
        catch (error) {
            console.error('Error fetching data:', error);
        }
    };   

    const ColorButton = styled(Button)<ButtonProps>(({ theme }) => ({
        color: "#ffffff",
        backgroundColor: "#2B3A44",
        fontWeight: 600,
        '&:hover': {
          backgroundColor: "#00EEAE",
          color: "#2B3A44"
        },
    }));

    return (
        <Box p={2} gap={4} sx={{ borderRadius: 2, border: '2px solid #8E99A8', maxWidth: 500 }}>
            <Typography variant="h5" component="h1" className={poppins.className}>Login to Gainwell PHM</Typography>
            <form onSubmit={handleLogin}>
                <Container>
                    <TextField sx={{ mt: 2, mb: 2 }} value={userName} onChange={(e) => setUserName(e.target.value)} placeholder='Username' />
                    <TextField sx={{ mt: 2, mb: 2, ml: 2 }} type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder='Password' />
                    <ColorButton variant="contained" type="submit">Login</ColorButton>                    
                </Container>                    
            </form>            
        </Box>
    );
};

export default Login;
