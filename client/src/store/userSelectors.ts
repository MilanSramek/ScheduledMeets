import { createSelector } from '@reduxjs/toolkit';

import { useSelector } from './hooks';
import { RootState } from './store';

const selectUser = createSelector(
  (state: RootState) => state.user,
  (user) => user
);
export const useUser = () => useSelector(selectUser);

const selectUserSignedIn = createSelector(selectUser, (user) => !!user);
export const useUserSignedIn = () => useSelector(selectUserSignedIn);
