import { styled } from '@mui/material';
import MuiDrawer from '@mui/material/Drawer';

export const Drawer = styled(MuiDrawer)(({ theme }) => ({
  '& .MuiDrawer-paperAnchorLeft': {
    boxSizing: 'border-box',
    overflowX: 'hidden',
    width: theme.spacing(7),
  },
}));
