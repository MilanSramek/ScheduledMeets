import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { User } from 'src/gql/graphql';

export const userSlice = createSlice({
  name: 'user',
  initialState: null as User | null,
  reducers: {
    set: (_, action: PayloadAction<User>) => action.payload,
  },
});

export const { set: setUser } = userSlice.actions;
export const userReducer = userSlice.reducer;
