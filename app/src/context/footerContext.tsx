import React, { createContext, useContext, useState, ReactNode } from 'react';

interface FooterContextProps {
    footerContent: ReactNode;
    setFooterContent: (content: ReactNode) => void;
}

const FooterContext = createContext<FooterContextProps | undefined>(undefined);

export const FooterProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [footerContent, setFooterContent] = useState<ReactNode>(null);

    return (
        <FooterContext.Provider value={{ footerContent, setFooterContent }}>
            {children}
        </FooterContext.Provider>
    );
};

export const useFooter = () => {
    const context = useContext(FooterContext);
    if (!context) {
        throw new Error('useFooter must be used within a FooterProvider');
    }
    return context;
};
