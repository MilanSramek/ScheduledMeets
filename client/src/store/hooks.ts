import {
  TypedUseSelectorHook,
  useSelector as useSelectorBase,
  useDispatch as useDispatchBase,
} from 'react-redux';

import { AppDispatch, RootState } from './store';

export const useDispatch: () => AppDispatch = useDispatchBase;
export const useSelector: TypedUseSelectorHook<RootState> = useSelectorBase;
