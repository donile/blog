psql \
    postgresql://${POSTGRES_USER}:${POSTGRES_PASSWORD}@localhost:5432 \
    -variable password="'${POSTGRES_BLOG_PASSWORD}'" \
    --file src/Blog.Database/init/init.sql
