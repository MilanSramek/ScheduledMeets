import { FC, useState } from 'react';
import { useTranslation } from 'react-i18next';
import MenuIcon from '@mui/icons-material/Menu';
import {
  AppBar,
  Box,
  Drawer,
  IconButton,
  Toolbar,
  Typography,
} from '@mui/material';

import { MainMenu } from './MainMenu';

export const AppFrame: FC = ({ children }) => {
  const { t } = useTranslation();
  const [menuOpen, setMenuOpen] = useState(false);
  return (
    <Box sx={{ display: 'flex' }}>
      <Drawer
        open={menuOpen}
        anchor="bottom"
        onClose={() => setMenuOpen(false)}
      >
        <MainMenu />
      </Drawer>
      <AppBar position="fixed" color="primary">
        <Toolbar>
          <IconButton
            edge="start"
            color="inherit"
            aria-label="open drawer"
            sx={{
              marginRight: '36px',
            }}
            onClick={() => setMenuOpen(true)}
          >
            <MenuIcon />
          </IconButton>
          <Typography
            component="h1"
            variant="h6"
            color="inherit"
            noWrap
            sx={{ flexGrow: 1 }}
          >
            {t('Dashboard')}
          </Typography>
        </Toolbar>
      </AppBar>
      {children}
    </Box>
  );
};
