import { FC } from 'react';
import { useUser } from 'store';

export const UserPage: FC = () => {
  const user = useUser();
  return <>Ahoj {user?.username} = blb</>;
};
