import { FC, useEffect, useRef } from 'react';
import { SignInWithGoogleButtonProps, ScriptStatus, Type } from './types';
import { useScript } from './useScript';

const googleClientScriptURL: string = 'https://accounts.google.com/gsi/client';

export const SignInWithGoogleButton: FC<SignInWithGoogleButtonProps> = ({
  clientId,
  callback,
}) => {
  const scriptState = useScript(googleClientScriptURL);
  const buttonTarget = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (scriptState !== ScriptStatus.ready) return;

    window.google.accounts.id.initialize({
      client_id: clientId,
      callback: ({ credential }) => callback(credential),
    });

    if (!buttonTarget.current) return;

    window.google.accounts.id.renderButton(buttonTarget.current, {
      type: Type.standard,
    });
  }, [scriptState, buttonTarget.current]);

  return <div ref={buttonTarget} />;
};
