import React from 'react';
import { Box, Button, Modal, Typography, IconButton, ButtonProps, styled } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';

interface ModalPopupProps {
    open: boolean;
    onClose: () => void;
    headerText: string;
    footerButtons: Array<{
        label: string;
        onClick: () => void;
    }>;
    children: React.ReactNode;
}

const ColorButton = styled(Button)<ButtonProps>(({ theme }) => ({
    color: "#ffffff",
    backgroundColor: "#2B3A44",
    fontWeight: 600,
    '&:hover': {
      backgroundColor: "#00EEAE",
      color: "#2B3A44"
    },
}));

const ModalPopup: React.FC<ModalPopupProps> = ({ open, onClose, headerText, footerButtons, children }) => {
  return (
    <div>
      <Modal open={open} onClose={onClose}>
        <Box sx={modalStyle}>
          <Box sx={headerStyle}>
            <Typography variant="h6" sx={{ lineHeight: 2 }}>{headerText}</Typography>
            <IconButton onClick={onClose} title='Close' aria-label='close'>
              <CloseIcon sx={{ color: "#ffffff", '&:hover': { color: '#00EEAE', } }} />
            </IconButton>
          </Box>
          <Box sx={contentStyle}>
            {children}
          </Box>
          <Box sx={footerStyle}>
            {footerButtons.map((button, index) => (
              <ColorButton key={index} onClick={button.onClick} sx={{ ml: index > 0 ? 1 : 0 }}>
                {button.label}
              </ColorButton>
            ))}
          </Box>
        </Box>
      </Modal>
    </div>
  );
};

const modalStyle = {
  position: 'absolute' as 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 600,
  bgcolor: '#FFFFFF',
  boxShadow: 24,
  //p: 4,
  borderRadius: 1,
};

const contentStyle = {
    p: 2,
}

const headerStyle = {
  display: 'flex',
  justifyContent: 'space-between',
  borderBottom: '1px solid #ddd',
  p: 1,
  backgroundColor: '#2B3A44',
  color: '#FFFFFF',
};

const footerStyle = {
  display: 'flex',
  justifyContent: 'flex-end',
  //mt: 2,
  borderTop: '1px solid #5A6978',
  p: 1,
  gap: 2,
  bgcolor: '#EBEDF5',
};

export default ModalPopup;
