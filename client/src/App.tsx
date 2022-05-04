import { ApolloProvider, useMutation } from '@apollo/client';
import React, { FC, useCallback } from 'react';
import GoogleOneTapLogin from 'react-google-one-tap-login';
import { client } from './apolloClient';
import { SIGN_IN } from './mutations.graphql';
import { SingIn, SingInVariables } from './__generated__/SingIn';

export const Test: FC = () => {
  const [signIn] = useMutation<SingIn, SingInVariables>(SIGN_IN);
  const callback = useCallback(async (arg: any) => {
    console.log(arg);
    const data = await signIn({ variables: { idToken: arg.credential } });
    console.log(data);
  }, []);

  return (
    <GoogleOneTapLogin
      googleAccountConfigs={{
        client_id:
          '1008686296759-t3tpolkuhj6s161c1tp4stiq8jfacece.apps.googleusercontent.com',
        callback,
      }}
    ></GoogleOneTapLogin>
  );
};

export const App: FC = () => {
  return (
    <ApolloProvider client={client}>
      <Test />
      <div>Ahoj světe!</div>
    </ApolloProvider>
  );
};
