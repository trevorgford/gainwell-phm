'use client';

import { useEffect } from 'react';
//import { useRouter } from 'next/router';
import { useRouter, usePathname } from 'next/navigation';
import { getAuthenticationContext } from '../context/AuthContext';
//import isClient from '../utils/isClient';

const requireAuthentication = () => {
    const { isAuthenticated, isAuthenticationChecked } = getAuthenticationContext();
    const router = useRouter();
    const pathname = usePathname();
    //const router = isClient() ? useRouter() : null;

    // useEffect(() => {
    //     if (isAuthenticationChecked && !isAuthenticated && pathname !== '/login') router.replace('/login');
    // }, [isAuthenticationChecked, isAuthenticated, router]);

    // useEffect(() => {
    //     if (!isAuthenticated && pathname !== '/login') router.replace('/login');
    // }, [isAuthenticated, router]);    

    return { isAuthenticated };
};

export default requireAuthentication;
