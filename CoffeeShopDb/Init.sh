#!/bin/bash

initialize_database() {

  success=0;
  while [ $success -ne 1 ]; do
    sleep 1s;
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'coffeeShopOwner!@' -d master -i /initialization/Init.sql \
      && success=1 \
      && echo \
      && echo 'Database initialization complete! You can ignore the Sqlcmd errors above. They are expected during initialization.' \
      && echo;
  done
}

initialize_database &
/opt/mssql/bin/sqlservr
