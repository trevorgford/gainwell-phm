'use client';

import { createContext, useContext, useEffect, useState, ReactNode } from 'react';

interface AuthContextType {
    isAuthenticated: boolean;
    isAuthenticationChecked: boolean;
    login: () => void;
    logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    //const [isAuthenticated, setIsAuthenticated] = useState(localStorage.getItem('auth') === 'true' || false);
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [isAuthenticationChecked, setIsAuthenticationChecked] = useState(false);

    useEffect(() => {
        // Check authentication state from localStorage
        const auth = localStorage.getItem('auth');
        setIsAuthenticated(auth === 'true');
        setIsAuthenticationChecked(true);
        //debugger;
    }, [isAuthenticated]);

    const login = () => {
        setIsAuthenticated(true);
        localStorage.setItem('auth', 'true');
    };

    const logout = () => {
        setIsAuthenticated(false);
        localStorage.removeItem('auth');
        localStorage.removeItem('token');
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, isAuthenticationChecked, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const getAuthenticationContext = () => {
    const context = useContext(AuthContext);
    if (!context) throw new Error('getAuthenticationContext must be used within an AuthProvider');
    //console.log('useAuth context:', context);
    return context;
};
