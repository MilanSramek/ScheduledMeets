import { FC } from 'react';
import { RouterProvider } from 'react-router-dom';
import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/integration/react';
import { ApolloProvider } from '@apollo/client';

import { store, persistor } from 'store';
import { client } from './apolloClient';
import { router } from './router';

export const App: FC = () => {
  return (
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <ApolloProvider client={client}>
          <RouterProvider router={router} />
        </ApolloProvider>
      </PersistGate>
    </Provider>
  );
};
