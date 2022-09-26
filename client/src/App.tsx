import { FC } from 'react';
import { ApolloProvider } from '@apollo/client';
import { client } from './apolloClient';
import { SignInPage } from './pages';

export const App: FC = () => {
  return (
    <ApolloProvider client={client}>
      <SignInPage />
    </ApolloProvider>
  );
};
