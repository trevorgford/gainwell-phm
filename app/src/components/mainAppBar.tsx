import React, { useState } from 'react';
import { AppBar, Toolbar, Typography, IconButton, Box, Card, CardContent, Modal } from '@mui/material';
import InfoIcon from '@mui/icons-material/Info';
import CloseIcon from '@mui/icons-material/Close';

import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "../app/globals.css";
import Head from 'next/head';
import Link from 'next/link';
import Image from "next/image";
import Container from '@mui/material/Container';
import CssBaseline from '@mui/material/CssBaseline';
import { Poppins } from 'next/font/google';
import LogoutButton from "./logoutButton";
import { LinearProgress, Stack, linearProgressClasses, styled } from "@mui/material";

interface AppBarProps {
  title?: string;
  memberDetails?: { name: string; id: number; };
}

const poppins = Poppins({
    subsets: ['latin'],
    weight: "500"
});

const MainAppBar: React.FC<AppBarProps> = ({ title, memberDetails }) => {
  const [open, setOpen] = useState(false);

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  return (
    <>
      <AppBar position="static">
        <Toolbar>
            <Image src="/GainwellLogo-Light.png" alt="Gainwell Logo" width={151} height={36} />
            <Typography variant="h4" component="div" sx={{ ml: 2, color: 'white' }} className={poppins.className}>
                &#x2022; PHM
            </Typography>
            {title && (
                <>
                    <Typography variant="h6" component="div" sx={{ ml: 10 }}>
                        {title}
                    </Typography>
                </>
            )}
            {memberDetails && (
                <>
                    <Typography variant="h6" component="div" sx={{ ml: 10 }}>
                        {memberDetails.name} ({memberDetails.id})
                    </Typography>
                    <IconButton color="inherit" onClick={handleOpen} title="View Member Details">
                        <InfoIcon />
                    </IconButton>
                </>
            )}
            <Box sx={{ flexGrow: 1 }} />
            <Box sx={{ alignItems: 'right' }}>
                <LogoutButton />
            </Box>
        </Toolbar>
      </AppBar>
      <Modal open={open} onClose={handleClose} aria-labelledby="member-details-modal" aria-describedby="member-details-description">
        <Card sx={{ maxWidth: 345, margin: 'auto', mt: '10%' }}>
          <CardContent>
            <Box sx={{ display: 'flex', justifyContent: 'space-between' }}>
              <Typography variant="h6" component="div">
                Member Details
              </Typography>
              <IconButton onClick={handleClose}>
                <CloseIcon />
              </IconButton>
            </Box>
            {memberDetails && (
              <>
                <Typography variant="body2" color="text.secondary">
                  Name: {memberDetails.name}
                </Typography>
              </>
            )}
          </CardContent>
        </Card>
      </Modal>
    </>
  );
};

export default MainAppBar;
