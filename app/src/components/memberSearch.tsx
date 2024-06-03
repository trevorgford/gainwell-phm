'use client';

import React, { useState } from 'react';
import TextField from '@mui/material/TextField';
import Button, { ButtonProps } from '@mui/material/Button';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import axiosInstance from "@/lib/axios";
import { Member } from '../types/Member';
import styled from '@emotion/styled';
import { Box } from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
//import { useRouter } from 'next/router';

const ColorButton = styled(Button)<ButtonProps>(({ theme }) => ({
    color: "#ffffff",
    backgroundColor: "#2B3A44",
    fontWeight: 600,
    '&:hover': {
      backgroundColor: "#00EEAE",
      color: "#2B3A44"
    },
}));

const MemberSearch: React.FC = () => {
    const [query, setQuery] = useState('');
    const [members, setMembers] = useState<Member[]>([]);
    const [loading, setLoading] = useState(false);
    const [searchPerformed, setSearchPerformed] = useState(false);

    const handleSearch = async () => {
        setLoading(true);
        try {
            const token = localStorage.getItem('token') || '';
            axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            //const response = await axiosInstance.get('/api/barriers');

            const response = await axiosInstance.get<Member[]>('/api/members/search', {
                params: { query },
            });
            setMembers(response.data);
            setSearchPerformed(true);
        } catch (error) {
            console.error('Error fetching members:', error);
        } finally {
            setLoading(false);
        }
    };

    const columns: GridColDef[] = [
        { field: 'id', headerName: 'Member ID', width: 100, renderCell: (params) => {
            //const router = useRouter();
        
            const handleClick = (e: { preventDefault: () => void; }) => {
              e.preventDefault();
              //router.push(`/member/${params.row.id}`);
              window.location.href = `/members/${params.row.id}`;
            };
        
            return (
              <a href={`/members/${params.row.id}`} onClick={handleClick}>
                {params.value}
              </a>
            );
          }, },
        { field: 'firstName', headerName: 'First Name', width: 150 },
        { field: 'lastName', headerName: 'Last Name', width: 150 },
      ];   

    return (
        <div>
            <TextField
                label="Search Members"
                variant="outlined"
                value={query}
                onChange={(e) => setQuery(e.target.value)}
                fullWidth
            />
            <Box sx={{ alignContent: 'right' }}>
                <ColorButton
                    variant="contained"
                    color="primary"
                    onClick={handleSearch}
                    disabled={loading}
                    sx={{ mt: 2 }}
                >
                    {loading ? 'Searching...' : 'Search'}
                </ColorButton>
            </Box>
            {searchPerformed && (
                <DataGrid rows={members} columns={columns} getRowId={(row) => row.id} sx={{ mt: 3 }} />
            )}
        </div>
    );
};

export default MemberSearch;
