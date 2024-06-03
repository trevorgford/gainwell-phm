'use client';

import { useRouter } from 'next/navigation';
//import { useAuth } from '../context/AuthContext';
import { useEffect } from 'react';

const withAuth = (WrappedComponent: React.FC) => {
    const Wrapper: React.FC = (props) => {
        //const { isAuthenticated, isAuthChecked } = useAuth();
        //const router = useRouter();

        useEffect(() => {
            //debugger;
            //if (!isAuthChecked) return;
            //debugger;
            //console.log('isAuthenticated:', isAuthenticated);
            //if (!isAuthenticated) router.push('/login');
        }, []);

        //if (isAuthChecked && !isAuthenticated) return null;
        return <WrappedComponent {...props} />;
    };

    return Wrapper;
};

export default withAuth;
