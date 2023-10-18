<script lang="ts">
	import type { HomeChoreDto } from '../../../nlp-api';

	export let chore: HomeChoreDto;
	let intervalModifier: string = '';
	let interval: string = '';

	const processChoreInfo = (_chore: HomeChoreDto) => {
		intervalModifier = chore.intervalModifier.toLowerCase();

		if (intervalModifier === 'weeks') {
			let weekValue = parseInt(chore.interval);
			interval = weekValue === 1 ? 'Weekly' : `Every ${weekValue} weeks`;
		} else if (intervalModifier === 'months') {
			let monthValue = parseInt(chore.interval);
			interval = monthValue === 1 ? 'Monthly' : `Every ${monthValue} months`;
		} else if (intervalModifier === 'days') {
			let daysValue = parseInt(chore.interval);
			interval = daysValue === 1 ? 'Daily' : `Every ${daysValue} days`;
		} else if (intervalModifier === 'daysofmonth') {
			let days = chore.interval.split(',').map((x) => parseInt(x));
			if (days.length === 1) {
				interval = `Day ${days[0]} monthly`;
			} else {
				interval = `Days "${days.join(', ')}" monthly`;
			}
		} else if (intervalModifier === 'daysofweek') {
			let dow = chore.interval.split(',');
			interval = dow.length === 1 ? `Every ${dow[0]}` : `Every ${dow.join(', ')} (weekly)`;
		} else {
			interval = `Unknown interval: ${intervalModifier}`;
		}
	};

	$: processChoreInfo(chore);
</script>

{interval}
