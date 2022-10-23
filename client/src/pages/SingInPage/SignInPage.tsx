import { FC, useCallback } from 'react';
import { useMutation } from '@apollo/client';
import { graphql } from 'gql';
import { useNavigate, useLocation } from 'react-router-dom';

import { env } from 'config';
import { SignInWithGoogleButton } from 'components';
import { setUser, useDispatch } from 'store';

const SIGN_IN = graphql(`
  mutation SignIn($idToken: String!) {
    signIn(idToken: $idToken) {
      id
      username
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
      const { data } = await signIn({ variables: { idToken } });
      if (!data?.signIn) return;

      dispatch(setUser(data?.signIn));

      const from = location.state?.from?.pathname || nextPath;

      navigate(from, { replace: true });
    },
    [location]
  );

  return (
    <SignInWithGoogleButton
      clientId={env.GOOGLE_CLIENT_ID}
      callback={makeSignIn}
    />
  );
};
