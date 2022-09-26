import { useState, useEffect } from 'react';
import { ScriptEventType, ScriptStatus } from './types';

const dataStatusAttributeName = 'data-status';

/* Original source: react-google-one-tap-login */
export const useScript = (src: string): string => {
  const [status, setStatus] = useState<ScriptStatus>(
    src ? ScriptStatus.loading : ScriptStatus.idle
  );

  useEffect(() => {
    if (!src) {
      setStatus(ScriptStatus.idle);
      return;
    }

    const script: HTMLScriptElement | null = document.querySelector(
      `script[src="${src}"]`
    );

    if (!!script) {
      setStatus(
        (script.getAttribute(dataStatusAttributeName) as ScriptStatus) ||
          ScriptStatus.idle
      );

      const setDataLoading = () => setStatus(ScriptStatus.ready);
      script.addEventListener(ScriptEventType.load, setDataLoading);

      const setDataError = () => setStatus(ScriptStatus.error);
      script.addEventListener(ScriptEventType.error, setDataError);

      return () => {
        script.removeEventListener(ScriptEventType.load, setDataLoading);
        script.removeEventListener(ScriptEventType.error, setDataError);
      };
    }

    /* Create a new script */
    const newScript = document.createElement('script');
    newScript.src = src;
    newScript.async = true;
    newScript.setAttribute(dataStatusAttributeName, ScriptStatus.loading);
    document.body.appendChild(newScript);

    const setDataLoading = ({ currentTarget }: Event) => {
      (currentTarget as HTMLScriptElement).setAttribute(
        dataStatusAttributeName,
        ScriptStatus.ready
      );
      setStatus(ScriptStatus.ready);
    };
    newScript.addEventListener(ScriptEventType.load, setDataLoading);

    const setDataError = ({ currentTarget }: Event) => {
      (currentTarget as HTMLScriptElement).setAttribute(
        dataStatusAttributeName,
        ScriptStatus.error
      );
      setStatus(ScriptStatus.error);
    };
    newScript.addEventListener(ScriptEventType.error, setDataError);

    return () => {
      newScript.removeEventListener(ScriptEventType.load, setDataLoading);
      newScript.removeEventListener(ScriptEventType.error, setDataError);
    };
  }, [src]);

  return status;
};
