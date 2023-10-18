<style>
	.options {
		display: flex;
		flex-wrap: wrap;
		justify-content: center;
	}
	.options .entry {
		padding: 0.2em;
		cursor: pointer;
		margin: 0.1em;
		min-width: 2em;
		text-align: center;
		font-weight: 600;
		border-radius: 0.5em;
	}
	.options.dow .entry {
		min-width: 3em;
	}
	.options .entry:hover {
		background-color: #69696954;
	}
	.options .entry.selected {
		background-color: #01754e78;
	}
</style>

<script lang="ts">
	import { onMount } from 'svelte';
	import { Input } from 'sveltestrap';

	export let modifier: string = 'Weeks';
	export let interval: string = '';

	class Dom {
		constructor(public day: number, public selected: boolean = false) {}
	}

	class Dow {
		constructor(public day: string, public selected: boolean = false) {}
	}

	let daysValue: number = 1;
	let weeksValue: number = 1;
	let monthsValue: number = 1;
	let daysOfMonth: Dom[] = [];
	let daysOfWeek: Dow[] = [];
	let debouncing: boolean = false;
	let debounceTimeout: number | undefined = undefined;

	const debounce = (debounceMs: number = 20) => {
		debouncing = true;
		if (debounceTimeout) clearTimeout(debounceTimeout);
		debounceTimeout = setTimeout(() => {
			debouncing = false;
		}, debounceMs);
	};

	const daysValueChanged = (_amount: number) => {
		if (modifier !== 'Days' || debouncing) return;
		interval = `${_amount}`;
	};

	const weeksValueChanged = (_amount: number) => {
		if (modifier !== 'Weeks' || debouncing) return;
		interval = `${_amount}`;
	};

	const monthsValueChanged = (_amount: number) => {
		if (modifier !== 'Months' || debouncing) return;
		interval = `${_amount}`;
	};

	const toggleDom = (dom: Dom) => {
		dom.selected = !dom.selected;
		daysOfMonth = daysOfMonth;
		interval = daysOfMonth
			.filter((x) => x.selected)
			.map((x) => x.day)
			.join(',');
	};

	const toggleDow = (dom: Dow) => {
		dom.selected = !dom.selected;
		daysOfWeek = daysOfWeek;
		interval = daysOfWeek
			.filter((x) => x.selected)
			.map((x) => x.day)
			.join(',');
	};

	const intervalChanged = (_mod: string, value: string) => {
		if (_mod === 'DaysOfMonth') {
			daysOfMonth = [];
			for (var i = 0; i < 31; i++) {
				daysOfMonth.push(new Dom(i + 1));
			}
			for (const day of value.split(',').map((x) => parseInt(x))) {
				if (isNaN(day)) continue;
				daysOfMonth.filter((x) => x.day === day)[0].selected = true;
			}
		} else if (_mod === 'DaysOfWeek') {
			daysOfWeek = [
				new Dow('Mon'),
				new Dow('Tue'),
				new Dow('Wed'),
				new Dow('Thu'),
				new Dow('Fri'),
				new Dow('Sat'),
				new Dow('Sun')
			];

			for (const day of value.split(',')) {
				if ((day?.length || 0) !== 3) continue;
				daysOfWeek.filter((x) => x.day === day)[0].selected = true;
			}
		} else if (_mod === 'Days') {
			daysValue = parseInt(value);
		} else if (_mod === 'Weeks') {
			weeksValue = parseInt(value);
		} else if (_mod === 'Months') {
			monthsValue = parseInt(value);
		}
	};

	$: daysValueChanged(daysValue);
	$: weeksValueChanged(weeksValue);
	$: monthsValueChanged(monthsValue);
	$: intervalChanged(modifier, interval);

	debounce(20);
	intervalChanged(modifier, interval);
</script>

<div class="d-flex">
	<div style="flex: 1; margin-right: 0.5em">
		<Input type="select" bind:value={modifier}>
			<option value="Days">Days</option>
			<option value="Weeks">Weeks</option>
			<option value="Months">Months</option>
			<option value="DaysOfMonth">DaysOfMonth</option>
			<option value="DaysOfWeek">DaysOfWeek</option>
		</Input>
	</div>

	{#if modifier === 'Days'}
		<Input type="number" min={1} bind:value={daysValue} style="flex: 1" />
	{:else if modifier === 'Weeks'}
		<Input type="number" min={1} bind:value={weeksValue} style="flex: 1" />
	{:else if modifier === 'Months'}
		<Input type="number" min={1} bind:value={monthsValue} style="flex: 1" />
	{:else if modifier === 'DaysOfMonth'}
		<div class="options" style="flex: 1">
			{#each daysOfMonth as dom}
				<!-- svelte-ignore a11y-click-events-have-key-events -->
				<span class="entry" class:selected={dom.selected} on:click={() => toggleDom(dom)}>
					{dom.day}
				</span>
			{/each}
		</div>
	{:else if modifier === 'DaysOfWeek'}
		<div class="options dow" style="flex: 1">
			{#each daysOfWeek as dow}
				<!-- svelte-ignore a11y-click-events-have-key-events -->
				<span class="entry" class:selected={dow.selected} on:click={() => toggleDow(dow)}>
					{dow.day}
				</span>
			{/each}
		</div>
	{:else}
		<div class="options" style="flex: 1">Invalid Entry</div>
	{/if}
</div>
