import { FC, useCallback } from 'react';
import { useMutation } from '@apollo/client';
import { graphql } from 'gql';
import { useNavigate, useLocation } from 'react-router-dom';
import { Avatar, Box, Container, Typography } from '@mui/material';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';

import { env } from 'config';
import { SignInWithGoogleButton } from 'components';
import { setUser, useDispatch } from 'store';
import { User } from 'src/gql/graphql';

const SIGN_IN = graphql(`
  mutation SignIn($input: SignInInput!) {
    signIn(input: $input) {
      user {
        id
        username
      }
    }
  }
`);

export const SignInPage: FC<{ nextPath: string }> = ({ nextPath }) => {
  const dispatch = useDispatch();

  const navigate = useNavigate();
  const location = useLocation();

  const [signIn] = useMutation(SIGN_IN);

  const makeSignIn = useCallback(
    async (idToken: string) => {
      const { data } = await signIn({ variables: { input: { idToken } } });
      if (!data?.signIn.user) return;

      dispatch(setUser(data?.signIn.user as User));

      const from = location.state?.from?.pathname || nextPath;

      navigate(from, { replace: true });
    },
    [location]
  );

  return (
    <Container component="main" maxWidth="xs">
      <Box
        sx={{
          marginTop: 8,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
        }}
      >
        <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Sign in
        </Typography>
        <Box sx={{ marginTop: 2 }}>
          <SignInWithGoogleButton
            clientId={env.GOOGLE_CLIENT_ID}
            callback={makeSignIn}
          />
        </Box>
      </Box>
    </Container>
  );
};
