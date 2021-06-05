---
layout: default
title: Posts
---

## Posts

<ul class="posts">
  {% for post in site.categories.posts %}
    <li>
      <div class="publish-date">
        <time datetime="{{ post.date | date: '%d/%m/%Y' }}">{{ post.date | date:'%d/%m/%Y' }}</time>
      </div>
      <a href="{{ post.url }}">{{ post.title }}</a>
    </li>
  {% endfor %}
</ul>
