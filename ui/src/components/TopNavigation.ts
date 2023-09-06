export interface NavbarLink {
  text: string;
  href?: string;
  children?: NavbarLink[];
  onClick?: () => void;
};
