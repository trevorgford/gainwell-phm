import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "../app/globals.css";
import React, { ReactNode } from 'react';
import Head from 'next/head';
import Link from 'next/link';
import Image from "next/image";
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import Box from '@mui/material/Box';
import CssBaseline from '@mui/material/CssBaseline';
import { Poppins } from 'next/font/google';
import LogoutButton from "./logoutButton";
import { LinearProgress, Stack, linearProgressClasses, styled } from "@mui/material";

const inter = Inter({ subsets: ["latin"] });
const poppins = Poppins({
    subsets: ['latin'],
    weight: "500"
});

type LayoutProps = {
  children: ReactNode;
};

// const BorderLinearProgress = styled(LinearProgress)(({ theme }) => ({
//   height: 5,
//   [`&.${linearProgressClasses.colorPrimary}`]: {
//     backgroundColor: '#2B3A44',
//   },
//   [`& .${linearProgressClasses.bar}`]: {
//     backgroundColor: '#00EEAE',
//   },
// }));

// const ProgressBar: React.FC = () => {
//   if (isLoading) {
//       return <>
//         <Stack sx={{ width: '100%' }}>
//           <BorderLinearProgress />
//           <span>Hey</span>
//         </Stack> 
//       </>;
//   }
// };

const Layout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
      <AppBar position="static" sx={{ backgroundColor: '#2B3A44' }}>
        <Toolbar>
            <Image src="/GainwellLogo-Light.png" alt="Gainwell Logo" width={151} height={36} />
            <Typography variant="h4" component="div" sx={{ ml: 2, color: 'white' }} className={poppins.className}>
                &#x2022; PHM
            </Typography>
            <Box sx={{ flexGrow: 1 }} />
            <Box sx={{ alignItems: 'right' }}>
                <LogoutButton />
            </Box>
        </Toolbar>
      </AppBar>
      <Container component="main" sx={{ flex: 1, py: 4 }}>
        {children}
      </Container> 
      <Box component="footer" sx={{ py: 2, textAlign: 'center', mt: 'auto' }}>
        <Typography variant="body2">Copyright © 2024 Gainwell Technologies LLC. All rights reserved.</Typography>
      </Box>
    </Box>
  );
};

export default Layout;
