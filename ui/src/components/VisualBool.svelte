<script lang="ts">
  export let value: boolean = false;
  export let classTrue: string = 'bi bi-check-lg';
  export let classFalse: string = 'bi bi-x';
  export let hideIfFalse: boolean = false;
  export let title: string = '';
  let activeClass: string = '';
  let visible: boolean = true;

  $: activeClass = value ? classTrue : classFalse;
  $: visible = hideIfFalse ? value : true;
  $: title = title || `${value}`;
</script>
{#if visible}<i class={activeClass} {title}></i>{/if}