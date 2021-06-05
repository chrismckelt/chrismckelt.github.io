---
layout: plain
title: Tags
permalink: /tags/
---

## Tags

<div class="tag-container">
    {% assign sorted_tags = site.tags | sort %} {% for tag in sorted_tags %} {% assign t = tag | first %}
    <a class="badge bg-mckelt-purple" href="#{{ t }}">{{ t }}</a>
    {% endfor %}
</div>

{% for tag in sorted_tags %}
{% assign t = tag | first %}
{% assign posts = tag | last %}

  <h4 id="{{ t }}" class="text-capitalize">{{ t }}</h4>

  <ul>
    {% for post in posts %}
      {% if post.tags contains t %}
      <li>
        <a href="{{ post.url }}">{{ post.title }}</a>
        <span class="tags"> - {{ post.date | date: "%B %-d, %Y"  }}</span>
      </li>
      {% endif %}
    {% endfor %}
  </ul>
{% endfor %}