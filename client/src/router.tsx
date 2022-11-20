import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  Outlet,
} from 'react-router-dom';

import { AppFrame, SignInPage, UserPage } from 'parts';
import { AuthBarrier } from 'components';
import { Suspense } from 'react';

export const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route path={'/sign-in'} element={<SignInPage nextPath={'/user'} />} />
      <Route
        path={'/'}
        element={
          <Suspense fallback={'blaaaaaaaaaaaaaaaaaaaaaa'}>
            <AuthBarrier signInPath={'/sign-in'}>
              <AppFrame>
                <Outlet />
              </AppFrame>
            </AuthBarrier>
          </Suspense>
        }
      >
        <Route path={'user'} element={<UserPage />} />
        <Route path={'/'} element={''} />
      </Route>
      <Route path={'*'} element={'Page not found.'} />
    </>
  )
);
