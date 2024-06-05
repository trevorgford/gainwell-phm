'use client';

import { AuthProvider } from '../context/AuthContext';
import requireAuthentication from '../hooks/requireAuthentication';
import './globals.css';
import Layout from '../components/layout';
import { ReactNode } from 'react';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import { AppProps } from 'next/app';
import { Poppins } from 'next/font/google';
import theme from '../themes/theme';
import { AppBar, Box, Container, Stack, Typography } from '@mui/material';
import { blue } from '@mui/material/colors';

type RootLayoutProps = {
  children: ReactNode;
};

const poppins = Poppins({
  subsets: ['latin'],
  weight: "500"
});

//const theme = createTheme();

const AuthenticationChecker: React.FC<{ children: ReactNode }> = ({ children }) => {
  const { isAuthenticated } = requireAuthentication();

  // if (!isAuthenticated && typeof window !== 'undefined' && window.location.pathname !== '/login') {
  //     return <div>Loading...</div>;
  // }

  return <>{children}</>;
};

const RootLayout: React.FC<RootLayoutProps> = ({ children }) => {
  return (
    <html lang="en">
        <head>
            <title>Gainwell PHM</title>  
        </head>
        <body>
            <ThemeProvider theme={theme}>
              <CssBaseline />
                <Layout>
                  <AuthProvider>
                    <AuthenticationChecker>
                      <Container>
                        {children}
                      </Container>
                    </AuthenticationChecker>
                  </AuthProvider>
                </Layout>
            </ThemeProvider>           
        </body>
    </html>
  );
};

export default RootLayout;
