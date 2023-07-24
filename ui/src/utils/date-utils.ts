export const fmtDateShort = (date: Date) => date.toISOString().split('T')[0];

export const humanTimeDiff = (date: Date) => {
  const now = new Date().getTime();
  const timeDiff = Math.floor((now - date.getTime()) / 1000); // seconds
  if(timeDiff < 60) return '< 1 min';
  if(timeDiff < 3600) return `${Math.floor(timeDiff / 60)} min(s) ago`;
  if(timeDiff < 86400) return `${Math.floor(timeDiff / 3600)} hours ago`;
  if(timeDiff < 604800) return `${Math.floor(timeDiff / 86400)} days ago`;
  if(timeDiff < 2419200) return `${Math.floor(timeDiff / 604800)} weeks ago`;
  return `${date.getFullYear()}-${(date.getMonth()+'').padStart(2,'0')}-${(date.getDate()+'').padStart(2,'0')}`;
}
