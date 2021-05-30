psql \
    postgresql://${POSTGRES_USER}:${POSTGRES_PASSWORD}@localhost:5432 \
    --variable password="'${POSTGRES_BLOG_PASSWORD}'" \
    --variable role="${POSTGRES_BLOG_USER}" \
    --variable database="${POSTGRES_BLOG_DATABASE}" \
    --file src/Blog.Database/init/init.sql
