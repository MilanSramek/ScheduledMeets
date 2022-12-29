import { FC, useCallback, useState } from 'react';
import { Divider, Fab, List, useMediaQuery } from '@mui/material';
import MenuTwoToneIcon from '@mui/icons-material/MenuTwoTone';
import DashboardIcon from '@mui/icons-material/GridViewTwoTone';
import ManageAccountIcon from '@mui/icons-material/ManageAccountsTwoTone';
import SettingsIcon from '@mui/icons-material/SettingsSuggestTwoTone';
import { useTheme } from '@mui/material/styles';
import { useTranslation } from 'react-i18next';
// import { theme } from 'src/theme';

import { Item } from './Item';
import { Drawer } from './Drawer';

interface MainMenuItem {
  path: string;
  crumb: string;
}

export const MainMenu: FC<{
  dashboard: MainMenuItem;
  user: MainMenuItem;
  settings: MainMenuItem;
}> = ({ dashboard, user, settings }) => {
  const { t } = useTranslation();

  const theme = useTheme();
  const matches = useMediaQuery(theme.breakpoints.down('sm'));

  const [menuOpen, setMenuOpen] = useState(false);
  const closeMenu = useCallback(() => setMenuOpen(false), []);
  const openMenu = useCallback(() => setMenuOpen(true), []);

  return (
    <>
      <Drawer
        open={menuOpen}
        anchor={!matches ? 'left' : 'bottom'}
        variant={!matches ? 'permanent' : 'temporary'}
        onClose={closeMenu}
      >
        <List component="nav">
          <Item
            path={dashboard.path}
            crumb={dashboard.crumb}
            caption={t('Dashboard')}
            icon={<DashboardIcon />}
            onClick={closeMenu}
          />
          <Divider />
          <Item
            path={user.path}
            crumb={user.crumb}
            caption={t('User')}
            icon={<ManageAccountIcon />}
            onClick={closeMenu}
          />
          <Item
            path={settings.path}
            crumb={settings.crumb}
            caption={t('Settings')}
            icon={<SettingsIcon />}
            onClick={closeMenu}
          />
        </List>
      </Drawer>
      {matches && (
        <Fab
          color="primary"
          aria-label="edit"
          sx={{ position: 'absolute', bottom: 16, right: 16 }}
          onClick={openMenu}
        >
          <MenuTwoToneIcon />
        </Fab>
      )}
    </>
  );
};
