import React from 'react';
import { Container, IconButton } from '@mui/material';
import LogoutIcon from '@mui/icons-material/Logout';

const LogoutButton: React.FC = () => {

    const handleLogout = () => {
        localStorage.removeItem('auth');
        localStorage.removeItem('token');
        window.location.href = '/login';
    };

    return (
    <Container>
        <IconButton onClick={handleLogout} color="primary" title='Logout' aria-label="logout">
            <LogoutIcon sx={{ color: '#ffffff', '&:hover': { color: '#00EEAE', }, }} />
        </IconButton>
    </Container>
    );
};

export default LogoutButton;
