import { FC } from 'react';
import { RouterProvider } from 'react-router-dom';
import { ApolloProvider } from '@apollo/client';

import { client } from './apolloClient';
import { router } from './router';

export const App: FC = () => {
  return (
    <ApolloProvider client={client}>
      <RouterProvider router={router}></RouterProvider>
    </ApolloProvider>
  );
};
