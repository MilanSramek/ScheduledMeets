import { createSlice, PayloadAction } from '@reduxjs/toolkit';
interface User {
  email?: string | null;
  id: number;
  nickname?: string | null;
  username: string;
}

export const userSlice = createSlice({
  name: 'user',
  initialState: null as User | null,
  reducers: {
    set: (_, action: PayloadAction<User>) => action.payload,
  },
});

export const { set: setUser } = userSlice.actions;
export const userReducer = userSlice.reducer;
