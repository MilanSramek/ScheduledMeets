import {
  createBrowserRouter,
  createRoutesFromElements,
  Outlet,
  Route,
} from 'react-router-dom';
import { SignInPage } from './pages/SingInPage';
import { Routes } from './routes';

export const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route
        path={Routes.root}
        element={
          <>
            "Ahoj"<Outlet></Outlet>
          </>
        }
      >
        <Route path={'bla'} element={<SignInPage />}></Route>
      </Route>
    </>
  )
);
