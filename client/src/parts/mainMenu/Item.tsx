import { FC, ReactNode, useCallback } from 'react';
import { ListItemButton, ListItemIcon, ListItemText } from '@mui/material';
import { useMatches, useNavigate } from 'react-router-dom';

interface MatchHandle {
  crumb: string;
}

export const Item: FC<{
  icon: ReactNode;
  caption: string;
  path: string;
  crumb: string;
  onClick: () => void;
}> = ({ icon, caption, path, crumb, onClick }) => {
  const navigate = useNavigate();
  const matches = useMatches();
  const selected = matches.some((match) => {
    const handle = match?.handle as MatchHandle;
    return handle?.crumb == crumb;
  });

  return (
    <ListItemButton
      onClick={useCallback(() => {
        navigate(path);
        onClick();
      }, [path, onClick])}
      selected={selected}
    >
      <ListItemIcon>{icon}</ListItemIcon>
      <ListItemText primary={caption} />
    </ListItemButton>
  );
};
