import { Divider, List, ListItemButton } from '@mui/material';
import DashboardIcon from '@mui/icons-material/GridViewTwoTone';
import ManageAccountIcon from '@mui/icons-material/ManageAccountsTwoTone';
import SettingsIcon from '@mui/icons-material/SettingsSuggestTwoTone';

export const MainMenu = () => {
  return (
    <List component="nav">
      <ListItemButton>
        <DashboardIcon />
      </ListItemButton>
      <Divider />
      <ListItemButton>
        <ManageAccountIcon />
      </ListItemButton>
      <ListItemButton>
        <SettingsIcon />
      </ListItemButton>
    </List>
  );
};
