import { ApolloProvider, useMutation, useQuery } from '@apollo/client';
import React, { FC, useCallback } from 'react';
import GoogleOneTapLogin from 'react-google-one-tap-login';
import { client } from './apolloClient';
import { SIGN_IN, TEST } from './mutations.graphql';
import { SingIn, SingInVariables } from './__generated__/SingIn';
import { Sen } from './__generated__/Sen';

export const Test: FC = () => {
  const [signIn] = useMutation<SingIn, SingInVariables>(SIGN_IN);
  const callback = useCallback(async (arg: any) => {
    debugger;
    console.log(arg);
    const data = await signIn({ variables: { idToken: arg.credential } });
    debugger;
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

export const Te: FC = () => {
  const { data } = useQuery<Sen>(TEST);

  return <>{data?.Sentence}</>;
};

export const App: FC = () => {
  return (
    <ApolloProvider client={client}>
      {/* <Test /> */}
      {/* <div>Ahoj světe!</div> */}
      <Te></Te>
    </ApolloProvider>
  );
};
