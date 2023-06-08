export const humanFileSize = (size: number) => {
  if(size < 1024) return `${size} bytes`;
  if(size < 1048576) return `${Math.round((size/1024) * 100)/100} Kb`;
  if(size < 1073741824) return `${Math.round((size/1048576) * 100)/100} Mb`;
  if(size < 1099511627776) return `${Math.round((size/1073741824) * 100)/100} Gb`;
  return `>> ${size} <<`;
}
