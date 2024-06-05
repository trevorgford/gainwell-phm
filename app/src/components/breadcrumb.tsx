// import React from 'react';
// import Link from 'next/link';
// import { Breadcrumbs, Typography, Container } from '@mui/material';
// import { useRouter, usePathname } from 'next/navigation';

// const Breadcrumb: React.FC = () => {
//   const router = useRouter();
//   const pathname = usePathname();
//   const pathSegments = pathname.split('/').filter(segment => segment !== '');

//   return (
//     <Container>
//       <Breadcrumbs aria-label="breadcrumb">
//         <Link href="/">
//             Home
//         </Link>
//         {pathSegments.map((segment, index) => {
//           const href = `/${pathSegments.slice(0, index + 1).join('/')}`;
//           const isLast = index === pathSegments.length - 1;
//           return (
//             <Link key={segment} href={href}>
//               <Typography color={isLast ? 'textPrimary' : 'inherit'}>{segment}</Typography>
//             </Link>
//           );
//         })}
//       </Breadcrumbs>
//     </Container>
//   );
// };

// export default Breadcrumb;

// components/Breadcrumb.tsx

import React from 'react';
import { Breadcrumbs, Typography, Container } from '@mui/material';
import { Link } from '@mui/material';
import { useRouter, usePathname } from 'next/navigation';
//import { useNavigation } from 'next/navigation';

// Define a list of valid paths from your application
const validPaths = ['/page1', '/page2', '/page3'];

const Breadcrumb: React.FC = () => {
  const router = useRouter();
  const pathname = usePathname();
  //const navigation = useNavigation();
  const pathSegments = pathname.split('/').filter(segment => segment !== '');

  return (
    <Container>
      <Breadcrumbs aria-label="breadcrumb">
        <Link href="/" onClick={(e) => {
          e.preventDefault();
          router.replace('/');
          //navigation.navigate('/');
        }}>
          Home
        </Link>
        {pathSegments.map((segment, index) => {
          const href = `/${pathSegments.slice(0, index + 1).join('/')}`;
          const isLast = index === pathSegments.length - 1;
          // Check if the current segment is a valid path
          const isValidPath = validPaths.includes(href);
          // Render the breadcrumb link only if it's a valid path
          return isValidPath ? (
            <Link key={segment} href={href} onClick={(e) => {
              e.preventDefault();
              router.replace(href);
              //navigation.navigate(href);
            }}>
              <Typography color={isLast ? 'textPrimary' : 'inherit'}>{segment}</Typography>
            </Link>
          ) : null;
        })}
      </Breadcrumbs>
    </Container>
  );
};

export default Breadcrumb;
