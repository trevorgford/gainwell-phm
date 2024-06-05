'use client';

import Image from "next/image";
import Link from "next/link";
//import withAuth from '../components/withAuth';
import { Box, Button, ButtonProps, Typography, styled } from "@mui/material";
import MemberSearch from "@/components/memberSearch";

const Home: React.FC = () => {
  return (
    <Box sx={{ mt: 2 }}>
      <Typography variant="h3" align="center" gutterBottom>
        Welcome to Gainwell PHM
      </Typography>
      <Typography variant="h5" align="left" gutterBottom>
        Search for a Member
      </Typography>
      <MemberSearch />
    </Box>
  );
};

//export default withAuth(Home);
export default Home;
