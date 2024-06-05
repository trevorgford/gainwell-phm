

import type { Metadata } from "next";
import { useRouter, usePathname } from "next/navigation";
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
import MainAppBar from "./mainAppBar";


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
  const router = useRouter();
  const pathname = usePathname();

  const getMemberDetails = () => {
    if(pathname === '/members/1') {
      return { name: 'Mickey Mouse', id: 1 }
    }
    else if(pathname === '/members/1/assessments/1') {
      return { name: 'Mickey Mouse', id: 1 }
    }

    return undefined;
  };

  const getTitle = () => {
    if(pathname === '/members/1') {
      return 'Member Record';
    }

    return undefined;
  };

  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
      <MainAppBar memberDetails={getMemberDetails()} />
      {/* <AppBar>
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
      </AppBar> */}
      <Container component="main" sx={{ flex: 1, pt: 2, pb: 4 }}>
        {children}
      </Container> 
      <Box component="footer" sx={{ py: 2, textAlign: 'center', mt: 'auto' }}>
        <Typography variant="body2">Copyright © 2024 Gainwell Technologies LLC. All rights reserved.</Typography>
      </Box>
    </Box>
  );
};

export default Layout;
