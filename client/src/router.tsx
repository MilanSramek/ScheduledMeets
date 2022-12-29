import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  Outlet,
} from 'react-router-dom';

import { AppFrame, MainMenu, SignInPage, UserPage } from 'parts';
import { AuthBarrier } from 'components';
import { Suspense } from 'react';

const routerSettings = {
  singIn: { path: '/sign-in' },
  dashboard: { path: '/', crumb: 'dashboard' },
  user: { path: '/user', crumb: 'user' },
  settings: { path: '/settings', crumb: 'settings' },
};

export const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route
        path={routerSettings.singIn.path}
        element={<SignInPage nextPath={routerSettings.dashboard.path} />}
      />
      <Route
        path={'/'}
        element={
          <Suspense fallback={'Signing in'}>
            <AuthBarrier signInPath={routerSettings.singIn.path}>
              <AppFrame
                menu={
                  <MainMenu
                    dashboard={routerSettings.dashboard}
                    user={routerSettings.user}
                    settings={routerSettings.settings}
                  />
                }
                content={<Outlet />}
              />
            </AuthBarrier>
          </Suspense>
        }
      >
        <Route
          path={routerSettings.user.path}
          element={<UserPage />}
          handle={{
            crumb: routerSettings.user.crumb,
          }}
        />
        <Route
          path={routerSettings.dashboard.path}
          element={'dashboard'}
          handle={{
            crumb: routerSettings.dashboard.crumb,
          }}
        />
        <Route
          path={routerSettings.settings.path}
          element={'settings'}
          handle={{
            crumb: routerSettings.settings.crumb,
          }}
        />
      </Route>
      <Route path={'*'} element={'Page not found.'} />
    </>
  )
);
