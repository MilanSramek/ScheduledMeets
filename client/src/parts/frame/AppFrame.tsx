import { FC, ReactNode } from 'react';
import { Box, Container, Grid, useMediaQuery, useTheme } from '@mui/material';

export const AppFrame: FC<{
  menu: ReactNode;
  content: ReactNode;
}> = ({ menu, content }) => {
  const theme = useTheme();
  const matches = useMediaQuery(theme.breakpoints.down('sm'));

  return (
    <Box sx={{ display: 'flex' }}>
      {menu}
      <Box
        component="main"
        sx={{
          backgroundColor: (theme) =>
            theme.palette.mode === 'light'
              ? theme.palette.grey[100]
              : theme.palette.grey[900],
          flexGrow: 1,
          height: '100vh',
          overflow: 'auto',
        }}
      >
        <Container sx={{ mt: 4, mb: 4 }}>
          <Grid container sx={{ pl: theme.spacing(matches ? 0 : 7) }}>
            <Grid item xs={12}>
              {content}
            </Grid>
          </Grid>
        </Container>
      </Box>
    </Box>
  );
};
