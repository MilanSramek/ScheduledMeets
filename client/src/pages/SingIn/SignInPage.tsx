import { FC, useCallback } from 'react';
import { SignInWithGoogleButton } from 'components';

export const SignInPage: FC = () => {
  const makeSignIn = useCallback((idToken: string) => {
    console.log(idToken);
  }, []);

  return (
    <>
      <SignInWithGoogleButton
        clientId="1008686296759-t3tpolkuhj6s161c1tp4stiq8jfacece.apps.googleusercontent.com"
        callback={makeSignIn}
      />
    </>
  );
};
