import { FC, useCallback } from 'react';
import { useMutation } from '@apollo/client';

import { env } from 'config';
import { SignInWithGoogleButton } from 'components';

import { SIGN_IN } from './mutation.graphql';
import { SignIn, SignInVariables } from './__generated__/SignIn';

export const SignInPage: FC = () => {
  const [signIn] = useMutation<SignIn, SignInVariables>(SIGN_IN);

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
