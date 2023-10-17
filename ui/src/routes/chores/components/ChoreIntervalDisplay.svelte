<script lang="ts">
	import type { HomeChoreDto } from '../../../nlp-api';

	export let chore: HomeChoreDto;
	let intervalModifier: string = '';
	let interval: string = '';

	const processChoreInfo = (_chore: HomeChoreDto) => {
		intervalModifier = chore.intervalModifier.toLowerCase();

		if (intervalModifier === 'weeks') {
			let weekValue = parseInt(chore.interval);
			interval = weekValue === 1 ? 'Occurs every week' : `Occurs every ${weekValue} weeks`;
		} else if (intervalModifier === 'months') {
			let monthValue = parseInt(chore.interval);
			interval = monthValue === 1 ? 'Occurs monthly' : `Occurs every ${monthValue} months`;
		} else if (intervalModifier === 'days') {
			let daysValue = parseInt(chore.interval);
			interval = daysValue === 1 ? 'Occurs daily' : `Occurs every ${daysValue} days`;
		} else if (intervalModifier === 'daysofmonth') {
			let days = chore.interval.split(',').map((x) => parseInt(x));
			if (days.length === 1) {
				interval = `Occurs on day ${days[0]} of every month`;
			} else {
				interval = `Occurs on the "${days.join(', ')}" of every month`;
			}
		} else if (intervalModifier === 'daysofweek') {
			let dow = chore.interval.split(',');
			interval =
				dow.length === 1
					? `Occurs every ${dow[0]} (weekly)`
					: `Occurs every ${dow.join(', ')} (weekly)`;
		} else {
			interval = `Unknown interval: ${intervalModifier}`;
		}
	};

	$: processChoreInfo(chore);
</script>

{interval}
