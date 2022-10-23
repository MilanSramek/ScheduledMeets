import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  Outlet,
} from 'react-router-dom';

import { SignInPage, UserPage } from 'pages';
import { AuthBarrier } from 'components';
import { path } from './path';

export const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route path={path.signIn} element={<SignInPage nextPath={'/user'} />} />
      <Route
        path={'/'}
        element={
          <AuthBarrier signInPath={path.signIn}>
            <Outlet />
          </AuthBarrier>
        }
      >
        <Route path={'user'} element={<UserPage />} />
        <Route path={'*'} element={'NotFound'} />
      </Route>
    </>
  )
);
