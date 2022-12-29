import { FC } from 'react';
import { RouterProvider } from 'react-router-dom';
import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/integration/react';
import { ApolloProvider } from '@apollo/client';

import { store, persistor } from 'store';
import { client } from './apolloClient';
import { router } from './router';
import { ThemeProvider } from '@emotion/react';
import { theme } from './theme';

import './i18next';
import { CssBaseline } from '@mui/material';

export const App: FC = () => {
  return (
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <ApolloProvider client={client}>
          <ThemeProvider theme={theme}>
            <CssBaseline />
            <RouterProvider router={router} />
          </ThemeProvider>
        </ApolloProvider>
      </PersistGate>
    </Provider>
  );
};
