import { FC } from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { Ternary, Truthy, Falsy } from 'react-ternary-operator';

import { useUser } from 'store';

export const AuthBarrier: FC<{ signInPath: string }> = ({
  children,
  signInPath: signInfPath,
}) => {
  const location = useLocation();
  const user = useUser();

  return (
    <Ternary condition={!!user}>
      <Truthy>{children}</Truthy>
      <Falsy>
        <Navigate to={signInfPath} state={{ from: location }} replace />;
      </Falsy>
    </Ternary>
  );
};
