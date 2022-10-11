import { FC, useCallback } from 'react';
import { useMutation } from '@apollo/client';

import { env } from 'config';
import { SignInWithGoogleButton } from 'components';
import { graphql } from 'gql';

export const SIGN_IN = graphql(`
  mutation SignIn($idToken: String!) {
    signIn(idToken: $idToken) {
      id
      username
    }
  }
`);

export const SignInPage: FC = () => {
  const [signIn] = useMutation(SIGN_IN);

  const makeSignIn = useCallback((idToken: string) => {
    console.log(idToken);
    signIn({ variables: { idToken } });
  }, []);

  return (
    <>
      <SignInWithGoogleButton
        clientId={env.GOOGLE_CLIENT_ID}
        callback={makeSignIn}
      />
    </>
  );
};
