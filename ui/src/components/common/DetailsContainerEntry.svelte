<script lang="ts">
	import moment from 'moment';

	export let icon: string;
	export let value: string | Date | number | undefined = undefined;

	$: isDate = value instanceof Date;
	$: isNumber = typeof value === 'bigint' || typeof value === 'number';
</script>

<div class="entry">
	<i class="bi {icon}" />
	{#if value}
		{#if isDate}
			{moment(value).format('YYYY-MM-DD')}
		{:else if isNumber}
			{value}
		{:else}
			{value}
		{/if}
	{:else}
		<slot />
	{/if}
</div>

<style>
	.entry {
		width: 30%;
		min-width: 200px;
		margin: 0.25em;
	}
</style>
